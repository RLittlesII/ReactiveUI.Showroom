using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using Rocket.Surgery.Airframe.Synthetic;
using Showroom.Base;
using Splat;

namespace Showroom.Search
{
    public class SearchListViewModel : ViewModelBase
    {
        private readonly IDrinkService _drinkDataService;
        private readonly ReadOnlyObservableCollection<ItemViewModel> _items;
        private readonly ObservableAsPropertyHelper<bool> _isRefreshing;
        private string _searchText;
        private ReadOnlyObservableCollection<ItemViewModel> _newlist;

        public SearchListViewModel(IDrinkService drinkService)
        {
            _drinkDataService = drinkService;

            Func<ItemViewModel, bool> search(string searchTerm) =>
                viewModel =>
                {
                    if (string.IsNullOrEmpty(searchTerm))
                    {
                        return true;
                    }

                    var lower = searchTerm.ToLower();
                    return viewModel.Title.ToLower().Contains(lower) ||
                           (viewModel.Description?.ToLower().Contains(lower) ?? false);
                };

            var searchChanged =
                this.WhenAnyValue(x => x.SearchText)
                    .Throttle(TimeSpan.FromMilliseconds(800), RxApp.TaskpoolScheduler)
                    .DistinctUntilChanged()
                    .Select(search);

            var items =
                _drinkDataService
                    .ChangeSet
                    .Transform(x => new ItemViewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Type = x.Type,
                        Description = x.Description
                    });

            items
                .MergeMany((item, id) => item.Remove)
                .InvokeCommand(this, x => x.Remove)
                .DisposeWith(ViewModelSubscriptions);

            var filter = items
                .AutoRefresh(x => x.Id)
                .DeferUntilLoaded()
                .Filter(searchChanged);

            filter
                .Sort(SortExpressionComparer<ItemViewModel>
                    .Descending(x => x.Type)
                    .ThenByAscending(x => x.Id))
                .Bind(out _items)
                .DisposeMany()
                .Subscribe()
                .DisposeWith(ViewModelSubscriptions);

            Add = ReactiveCommand.CreateFromObservable<EventArgs, Unit>(ExecuteAdd);
            Refresh = ReactiveCommand.CreateFromObservable<EventArgs, Unit>(ExecuteRefresh);
            Remove = ReactiveCommand.CreateFromObservable(ExecuteRemove, Observable.Return(true));

            this.WhenAnyObservable(x => x.Refresh.IsExecuting)
                .StartWith(false)
                .DistinctUntilChanged()
                .ToProperty(this, nameof(IsRefreshing), out _isRefreshing)
                .DisposeWith(ViewModelSubscriptions);
        }

        public ReactiveCommandBase<PropertyValue<ItemViewModel, string>, object> Save { get; set; }

        public string Id { get; }

        public bool IsRefreshing => _isRefreshing.Value;

        public ReactiveCommand<EventArgs, Unit> Add { get; set; }

        public ReactiveCommand<EventArgs, Unit> Refresh { get; set; }

        public ReactiveCommand<Unit, Unit> Remove { get; set; }

        public ReadOnlyObservableCollection<ItemViewModel> Items => _items;

        public string SearchText
        {
            get => _searchText;
            set => this.RaiseAndSetIfChanged(ref _searchText, value);
        }

        private IObservable<Unit> ExecuteAdd(EventArgs args) =>
            Observable
                .Create<Unit>(observer =>
                    Interactions
                        .AddItem
                        .Handle(Unit.Default)
                        .ObserveOn(RxApp.MainThreadScheduler)
                        .Subscribe(observer)
                        .DisposeWith(ViewModelSubscriptions));

        private IObservable<Unit> ExecuteRefresh(EventArgs args) =>
            Observable
                .Create<Unit>(observer =>
                    _drinkDataService
                        .Read()
                        .Select(x => Unit.Default)
                        .Delay(TimeSpan.FromSeconds(2), RxApp.TaskpoolScheduler)
                        .ObserveOn(RxApp.MainThreadScheduler)
                        .Subscribe(observer)
                        .DisposeWith(ViewModelSubscriptions));

        private IObservable<Unit> ExecuteRemove() => _drinkDataService.Delete(Guid.Empty);
    }
}