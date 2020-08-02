using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DynamicData;
using ReactiveUI;
using Sextant;
using Showroom.Base;
using Showroom.Coffee;
using Splat;

namespace Showroom.CollectionView
{
    public class DrinkCollectionViewModel : ViewModelBase
    {
        private readonly ICoffeeService _coffeeService;
        private readonly IParameterViewStackService _viewStackService;
        private readonly ReadOnlyObservableCollection<DrinkViewModel> _coffeeList;

        public DrinkCollectionViewModel()
        {
            _viewStackService = Locator.Current.GetService<IParameterViewStackService>();
            _coffeeService = Locator.Current.GetService<ICoffeeService>();

            CoffeeDetails = ReactiveCommand.CreateFromObservable<DrinkViewModel, Unit>(ExecuteNavigate).DisposeWith(ViewModelSubscriptions);

            _coffeeService
                .ChangeSet
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .Transform(x => new DrinkViewModel(x.Id, x.Name, x.Species, x.Regions, x.Image))
                // .Sort(SortExpressionComparer<DrinkViewModel>.Ascending(p => p.Name))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _coffeeList)
                .DisposeMany()
                .Subscribe()
                .DisposeWith(ViewModelSubscriptions);
        }

        public ReactiveCommand<DrinkViewModel, Unit> CoffeeDetails { get; set; }

        public ReadOnlyObservableCollection<DrinkViewModel> Coffee => _coffeeList;

        protected override async Task ExecuteInitializeData() => await _coffeeService.Read();

        private IObservable<Unit> ExecuteNavigate(DrinkViewModel viewModel) =>
            Observable
                .Return(Unit.Default)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Do(_ =>
                    _viewStackService
                        .PushPage<CoffeeDetailViewModel>(new NavigationParameter {{"Id", viewModel.Id}})
                        .ObserveOn(RxApp.MainThreadScheduler)
                        .Subscribe());
    }
}