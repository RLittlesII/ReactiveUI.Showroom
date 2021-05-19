using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DynamicData;
using ReactiveUI;
using Rocket.Surgery.Airframe.ViewModels;
using Sextant;
using Sextant.Plugins.Popup;
using Showroom.ListView;
using Splat;

namespace Showroom.CollectionView
{
    public class DrinkCollectionViewModel : NavigableViewModelBase
    {
        private readonly ICoffeeService _coffeeService;
        private readonly IPopupViewStackService _viewStackService;
        private readonly ReadOnlyObservableCollection<DrinkViewModel> _coffeeList;

        public DrinkCollectionViewModel()
        {
            _viewStackService = Locator.Current.GetService<IPopupViewStackService>();
            _coffeeService = Locator.Current.GetService<ICoffeeService>();

            CoffeeDetails = ReactiveCommand.CreateFromObservable<DrinkViewModel, Unit>(ExecuteNavigate).DisposeWith(Garbage);

            _coffeeService
                .ChangeSet
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .Transform(x => new DrinkViewModel(x.Id, x.Name, x.Species, x.Regions, x.Image))
                // .Sort(SortExpressionComparer<DrinkViewModel>.Ascending(p => p.Name))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _coffeeList)
                .DisposeMany()
                .Subscribe()
                .DisposeWith(Garbage);
        }

        public ReactiveCommand<DrinkViewModel, Unit> CoffeeDetails { get; set; }

        public ReadOnlyObservableCollection<DrinkViewModel> Coffee => _coffeeList;

        protected override IObservable<Unit> ExecuteInitialize() => _coffeeService.Read().Select(x => Unit.Default);

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