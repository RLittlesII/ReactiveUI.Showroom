using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DynamicData;
using ReactiveUI;
using Rocket.Surgery.Airframe.Synthetic;
using Rocket.Surgery.Airframe.ViewModels;

namespace Showroom.CollectionView
{
    public class SearchCollectionViewModel : NavigableViewModelBase
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
                .DisposeWith(Garbage);

            Add = ReactiveCommand.CreateFromObservable<EventArgs, Unit>(ExecuteAdd).DisposeWith(Garbage);
            Refresh = ReactiveCommand.CreateFromObservable<EventArgs, Unit>(ExecuteRefresh).DisposeWith(Garbage);
            Remove = ReactiveCommand.CreateFromObservable(ExecuteRemove, Observable.Return(true)).DisposeWith(Garbage);
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

        protected override IObservable<Unit> ExecuteInitialize() =>
            _drinkService.Read().Select(_ => Unit.Default);

        private IObservable<Unit> ExecuteAdd(EventArgs args) =>
            Observable
                .Create<Unit>(observer =>
                    Interactions
                        .AddItem
                        .Handle(Unit.Default)
                        .ObserveOn(RxApp.MainThreadScheduler)
                        .Subscribe(observer)
                        .DisposeWith(Garbage));

        private IObservable<Unit> ExecuteRefresh(EventArgs args) =>
            Observable
                .Create<Unit>(observer =>
                    _drinkService
                        .Read()
                        .Select(x => Unit.Default)
                        .Delay(TimeSpan.FromSeconds(2), RxApp.TaskpoolScheduler)
                        .ObserveOn(RxApp.MainThreadScheduler)
                        .Subscribe(observer)
                        .DisposeWith(Garbage));

        private IObservable<Unit> ExecuteRemove() => _drinkService.Delete(Guid.Empty);
    }
}