using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using Showroom.Base;
using Showroom.CollectionView.Scroll;
using Showroom.Scroll;

namespace Showroom.CollectionView.Refresh
{
    public class RefreshCollectionViewModel : ViewModelBase
    {
        private readonly IInventoryDataService _inventoryDataService;
        private readonly ReadOnlyObservableCollection<RefreshItemViewModel> _inventory;
        private readonly BehaviorSubject<IPageRequest> _pagingSubject;
        private readonly int _pageSize = 100;
        private readonly ObservableAsPropertyHelper<bool> _isLoading;
        private ObservableAsPropertyHelper<bool> _isRefreshing;

        public RefreshCollectionViewModel(IInventoryDataService inventoryDataService)
        {
            _inventoryDataService = inventoryDataService;
            
            _pagingSubject = new BehaviorSubject<IPageRequest>(new PageRequest(0, _pageSize));

            _inventoryDataService
                .ChangeSet
                .Transform(x => new RefreshItemViewModel(x))
                .Bind(out _inventory)
                .DisposeMany()
                .Subscribe()
                .DisposeWith(ViewModelSubscriptions);

            this.WhenAnyObservable(x => x.Load.IsExecuting,
                    x => x.Refresh.IsExecuting,
                    (load, refresh) => load || refresh)
                .DistinctUntilChanged()
                .ToProperty(this, nameof(Load), out _isLoading, deferSubscription: true);

            Load = ReactiveCommand.CreateFromObservable(ExecuteLoad);
            Refresh = ReactiveCommand.CreateFromObservable<int, Unit>(ExecuteRefresh);
            Refresh
                .IsExecuting
                .ToProperty(this, nameof(Refreshing), out _isRefreshing);
        }

        private IObservable<Unit> ExecuteRefresh(int count)
        {
            return Observable.Return(Unit.Default).Delay(TimeSpan.FromSeconds(5));
        }

        /// <summary>
        /// This was hard.
        /// </summary>
        public ReactiveCommand<Unit, Unit> Load { get; set; }

        public ReactiveCommand<int, Unit> Refresh { get; }

        public ReadOnlyObservableCollection<RefreshItemViewModel> Items => _inventory;

        protected override async Task ExecuteInitializeData() => await _inventoryDataService.Read();

        // TODO: [rlittlesii: October 02, 2020] Why not have the _isLoading be the overrideable value?!
        public override bool IsLoading => _isLoading.Value;

        public bool Refreshing => _isRefreshing.Value;
        private IObservable<Unit> ExecuteLoad() =>
            Observable
                .Create<Unit>(observer =>
                {
                    _pagingSubject.OnNext(new PageRequest(1, _pageSize));

                    return _inventoryDataService
                        .Read()
                        .Select(x => Unit.Default)
                        .Subscribe(observer);
                });
    }
}