using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using Rocket.Surgery.Airframe.ViewModels;

namespace Showroom.Scroll
{
    public class InfiniteScrollViewModel : NavigableViewModelBase
    {
        private readonly int pageSize = 20;
        private readonly ReadOnlyObservableCollection<InventoryItemViewModel> _items;
        private readonly ObservableAsPropertyHelper<bool> _isRefreshing;
        private readonly ObservableAsPropertyHelper<bool> _isLoading;
        private readonly BehaviorSubject<IPageRequest> _pagingSubject;
        private readonly IInventoryDataService _inventoryDataService;
        private string _searchText;

        public InfiniteScrollViewModel(IInventoryDataService inventoryDataService)
        {
            _inventoryDataService = inventoryDataService;
            _pagingSubject = new BehaviorSubject<IPageRequest>(new PageRequest(0, pageSize));

            Func<InventoryItemViewModel, bool> Search(string searchTerm) =>
                viewModel =>
                {
                    if (string.IsNullOrEmpty(searchTerm))
                    {
                        return true;
                    }

                    var lower = searchTerm.ToLower();
                    return viewModel.Brand.ToLower().Contains(lower) || (viewModel.Coffee?.ToLower().Contains(lower) ?? false);
                };

            var searchChanged =
                this.WhenAnyValue(x => x.SearchText)
                    .Throttle(TimeSpan.FromMilliseconds(800), RxApp.TaskpoolScheduler)
                    .DistinctUntilChanged()
                    .Select(Search);

            _inventoryDataService
                .ChangeSet
                .Transform(x =>
                    new InventoryItemViewModel
                    {
                        Id = x.Id,
                        Brand = x.BrandName,
                        Coffee = x.CoffeeName,
                        Roast = x.Roast,
                        Packaging = x.Packaging
                    })
                .AutoRefresh(x => x.Id)
                .DeferUntilLoaded()
                .Filter(searchChanged)
                .Sort(SortExpressionComparer<InventoryItemViewModel>.Descending(x => x.Roast))
                .Page(_pagingSubject.AsObservable())
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _items)
                .DisposeMany()
                .Subscribe()
                .DisposeWith(Garbage);

            this.WhenAnyObservable(x => x.Refresh.IsExecuting)
                .StartWith(false)
                .DistinctUntilChanged()
                .ToProperty(this, nameof(IsRefreshing), out _isRefreshing, deferSubscription: true)
                .DisposeWith(Garbage);

            this.WhenAnyObservable(x => x.Load.IsExecuting)
                .ToProperty(this, nameof(IsLoading), out _isLoading, deferSubscription: true)
                .DisposeWith(Garbage);

            Refresh = ReactiveCommand.CreateFromObservable<EventArgs, Unit>(ExecuteRefresh);
            Load = ReactiveCommand.CreateFromObservable<int, Unit>(ExecuteLoad);

            _pagingSubject.DisposeWith(Garbage);
        }

        public string Id { get; }

        public override bool IsLoading => _isLoading.Value;

        public bool IsRefreshing => _isRefreshing.Value;

        public ReactiveCommand<int, Unit> Load { get; set; }

        public ReactiveCommand<EventArgs, Unit> Refresh { get; set; }

        public ReadOnlyObservableCollection<InventoryItemViewModel> Items => _items;

        public string SearchText
        {
            get => _searchText;
            set => this.RaiseAndSetIfChanged(ref _searchText, value);
        }

        private IObservable<Unit> ExecuteRefresh(EventArgs args) =>
            Observable
                .Create<Unit>(observer =>
                    _inventoryDataService
                        .Read()
                        .Select(x => Unit.Default)
                        .Delay(TimeSpan.FromSeconds(2), RxApp.TaskpoolScheduler)
                        .ObserveOn(RxApp.MainThreadScheduler)
                        .Subscribe(observer));

        private IObservable<Unit> ExecuteLoad(int arg) =>
            Observable
                .Create<Unit>(observer =>
                {
                    _pagingSubject.OnNext(new PageRequest(1, arg + 1 + pageSize));
                    return _inventoryDataService
                        .Read()
                        .Select(x => Unit.Default)
                        .Subscribe(observer);
                });
    }
}