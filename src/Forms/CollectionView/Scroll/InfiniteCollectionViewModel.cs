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
using Showroom.Scroll;

namespace Showroom.CollectionView.Scroll
{
    public class InfiniteCollectionViewModel : NavigableViewModelBase
    {
        private readonly IInventoryDataService _inventoryDataService;
        private ReadOnlyObservableCollection<InfiniteItemViewModel> _inventory;
        private BehaviorSubject<IPageRequest> _pagingSubject;
        private int _pageSize = 20; 

        public InfiniteCollectionViewModel(IInventoryDataService inventoryDataService)
        {
            _inventoryDataService = inventoryDataService;
            
            _pagingSubject = new BehaviorSubject<IPageRequest>(new PageRequest(0, _pageSize));

            _inventoryDataService
                .ChangeSet
                .Transform(x => new InfiniteItemViewModel(x))
                .Sort(SortExpressionComparer<InfiniteItemViewModel>.Ascending(x => x.Id))
                .Page(_pagingSubject.AsObservable())
                .Bind(out _inventory)
                .DisposeMany()
                .Subscribe()
                .DisposeWith(Garbage);

            Load = ReactiveCommand.CreateFromObservable(ExecuteLoad);
        }

        /// <summary>
        /// This was hard.
        /// </summary>
        public ReactiveCommand<Unit, Unit> Load { get; set; }

        public ReadOnlyObservableCollection<InfiniteItemViewModel> Items => _inventory;

        protected override IObservable<Unit> ExecuteInitialize() =>
            _inventoryDataService.Read().Select(x => Unit.Default);

        private IObservable<Unit> ExecuteLoad() =>
            Observable
                .Create<Unit>(observer =>
                {
                    _pagingSubject.OnNext(new PageRequest(1, _pageSize + _pageSize));

                    return _inventoryDataService
                        .Read()
                        .Select(x => Unit.Default)
                        .Subscribe(observer);
                });
    }
}