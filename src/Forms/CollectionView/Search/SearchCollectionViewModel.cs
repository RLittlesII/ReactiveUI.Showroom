using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DynamicData;
using DynamicData.Binding;
using DynamicData.PLinq;
using ReactiveUI;
using Rocket.Surgery.Airframe.Synthetic;
using Showroom.Base;

namespace Showroom.CollectionView
{
    public class SearchCollectionViewModel : ViewModelBase
    {
        private readonly IDrinkService _drinkService;
        private readonly ReadOnlyObservableCollection<ItemViewModel> _items;
        private string _searchText;

        public SearchCollectionViewModel(IDrinkService drinkService)
        {
            _drinkService = drinkService;

            Func<ItemViewModel, bool> search(string searchTerm) =>
                viewModel =>
                {
                    if (string.IsNullOrEmpty(searchTerm))
                    {
                        return true;
                    }

                    var lower = searchTerm.ToLower();
                    return viewModel.Title.ToLower().Contains(lower) || (viewModel.Description?.ToLower().Contains(lower) ?? false);
                };

            var searchChanged =
                this.WhenAnyValue(x => x.SearchText)
                    .Throttle(TimeSpan.FromMilliseconds(800), RxApp.TaskpoolScheduler)
                    .DistinctUntilChanged()
                    .Select(search);

            _drinkService
                .ChangeSet
                .Transform(x => new ItemViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Type = x.Type,
                    Description = x.Description
                })
                .AutoRefresh(x => x.Id)
                .DeferUntilLoaded()
                .Filter(searchChanged)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _items)
                .DisposeMany()
                .Subscribe()
                .DisposeWith(ViewModelSubscriptions);

            Add = ReactiveCommand.CreateFromObservable<EventArgs, Unit>(ExecuteAdd).DisposeWith(ViewModelSubscriptions);
            Refresh = ReactiveCommand.CreateFromObservable<EventArgs, Unit>(ExecuteRefresh).DisposeWith(ViewModelSubscriptions);
            Remove = ReactiveCommand.CreateFromObservable(ExecuteRemove, Observable.Return(true)).DisposeWith(ViewModelSubscriptions);
        }

        public ReactiveCommand<EventArgs, Unit> Add { get; set; }

        public ReactiveCommand<EventArgs, Unit> Refresh { get; set; }

        public ReactiveCommand<Unit, Unit> Remove { get; set; }

        public ReadOnlyObservableCollection<ItemViewModel> Items => _items;

        public string SearchText
        {
            get => _searchText;
            set => this.RaiseAndSetIfChanged(ref _searchText, value);
        }

        protected override async Task ExecuteInitializeData() => await _drinkService.Read();

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
                    _drinkService
                        .Read()
                        .Select(x => Unit.Default)
                        .Delay(TimeSpan.FromSeconds(2), RxApp.TaskpoolScheduler)
                        .ObserveOn(RxApp.MainThreadScheduler)
                        .Subscribe(observer)
                        .DisposeWith(ViewModelSubscriptions));

        private IObservable<Unit> ExecuteRemove() => _drinkService.Delete(Guid.Empty);
    }
}