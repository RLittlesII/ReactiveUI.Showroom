using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using Splat;

namespace Showroom.Scroll
{
    public class InfiniteLoadViewModel : ViewModelBase
    {
        private readonly IOrderService _hubConnectionService;
        private readonly ReadOnlyObservableCollection<LoadDataItemViewModel> _orders;
        private Subject<PageRequest> _pageRequests = new Subject<PageRequest>();

        public InfiniteLoadViewModel()
        {
            _hubConnectionService = Locator.Current.GetService<IOrderService>();

            _hubConnectionService
                .Orders
                .Connect()
                .Publish()
                .RefCount()
                .Transform(x => 
                    new LoadDataItemViewModel
                    {
                        Name = x.Name,
                        Ordered = x.DrinkName,
                        Size = x.Size.ToString()
                    })
                .Sort(SortExpressionComparer<LoadDataItemViewModel>.Ascending(x => x.Size))
                .Page(_pageRequests.AsObservable())
                .Bind(out _orders)
                .DisposeMany()
                .Subscribe();

            InitializeData = ReactiveCommand.CreateFromTask(ExecuteInitialize);
            LoadData = ReactiveCommand.CreateFromObservable<int, Unit>(ExecuteLoadData);
        }

        public ReactiveCommand<Unit, Unit> InitializeData { get; set; }

        public ReactiveCommand<int, Unit> LoadData { get; set; }

        public ReadOnlyObservableCollection<LoadDataItemViewModel> Orders => _orders;

        private async Task ExecuteInitialize() => await _hubConnectionService.GetOrders();

        private IObservable<Unit> ExecuteLoadData(int currentIndex)
        {
            var request = new PageRequest(currentIndex + 1, 25);

            _pageRequests.OnNext(request);

            return Observable.Return(Unit.Default);
        }
    }
}