using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using Rocket.Surgery.Airframe.ViewModels;
using Sextant;
using Sextant.Plugins.Popup;

namespace Showroom.ListView
{
    public class CoffeeListViewModel : NavigableViewModelBase
    {
        private readonly ICoffeeService _coffeeService;
        private readonly IPopupViewStackService _viewStackService;
        private readonly ReadOnlyObservableCollection<CoffeeCellViewModel> _coffeeList;

        public CoffeeListViewModel(IPopupViewStackService parameterViewStackService, ICoffeeService coffeeService)
        {
            _viewStackService = parameterViewStackService;
            _coffeeService = coffeeService;

            CoffeeDetails = ReactiveCommand.CreateFromObservable<CoffeeCellViewModel, Unit>(ExecuteNavigate);

            Refresh = ReactiveCommand.CreateFromTask(ExecuteRefresh);

            _coffeeService
                .ChangeSet
                .Transform(x => new CoffeeCellViewModel(x.Id, x.Name, x.Species, x.Regions, x.Image))
                .Sort(SortExpressionComparer<CoffeeCellViewModel>.Ascending(p => p.Name))
                .Bind(out _coffeeList)
                .DisposeMany()
                .Subscribe()
                .DisposeWith(Garbage);

            CoffeeDetails = ReactiveCommand.CreateFromObservable<CoffeeCellViewModel, Unit>(ExecuteNavigate).DisposeWith(Garbage);

        }

        private async Task ExecuteRefresh() => await _coffeeService.Read();

        public ReactiveCommand<CoffeeCellViewModel, Unit> CoffeeDetails { get; set; }

        public ReactiveCommandBase<Unit, Unit> Refresh { get; set; }

        public ReadOnlyObservableCollection<CoffeeCellViewModel> Coffee => _coffeeList;

        protected override IObservable<Unit> ExecuteInitialize() => _coffeeService.Read().Select(_ => Unit.Default);

        private IObservable<Unit> ExecuteNavigate(CoffeeCellViewModel viewModel) =>
            _viewStackService.PushPage<CoffeeDetailViewModel>(new NavigationParameter { { "Id", viewModel.Id } });
    }
}