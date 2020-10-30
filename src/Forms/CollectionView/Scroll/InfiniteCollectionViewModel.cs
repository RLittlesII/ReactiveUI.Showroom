using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using DynamicData;
using DynamicData.Binding;
using DynamicData.PLinq;
using ReactiveUI;
using Showroom.Base;
using Showroom.Scroll;

namespace Showroom.CollectionView.Scroll
{
    public class InfiniteCollectionViewModel : ViewModelBase
    {
        private readonly IInventoryDataService _inventoryDataService;
        private readonly ReadOnlyObservableCollection<InfiniteItemViewModel> _inventory;
        private readonly BehaviorSubject<IPageRequest> _pagingSubject;
        private readonly int _pageSize = 20; 

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
                .DisposeWith(ViewModelSubscriptions);

            Load = ReactiveCommand.CreateFromObservable(ExecuteLoad);
        }

        /// <summary>
        /// This was hard.
        /// </summary>
        public ReactiveCommand<Unit, Unit> Load { get; set; }

        public ReadOnlyObservableCollection<InfiniteItemViewModel> Items => _inventory;

        protected override async Task ExecuteInitializeData() => await _inventoryDataService.Read();

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