using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using Sextant;
using Showroom.Base;

namespace Showroom.Coffee
{
    public class CoffeeListViewModel : ViewModelBase
    {
        private readonly ICoffeeService _coffeeService;
        private readonly IParameterViewStackService _viewStackService;
        private readonly ReadOnlyObservableCollection<CoffeeCellViewModel> _coffeeList;

        public CoffeeListViewModel(IParameterViewStackService parameterViewStackService, ICoffeeService coffeeService)
        {
            _viewStackService = parameterViewStackService;
            _coffeeService = coffeeService;

            CoffeeDetails = ReactiveCommand.CreateFromObservable<CoffeeCellViewModel, Unit>(ExecuteNavigate).DisposeWith(ViewModelSubscriptions);

            Refresh = ReactiveCommand.CreateFromTask(ExecuteRefresh).DisposeWith(ViewModelSubscriptions);

            _coffeeService
                .ChangeSet
                .Transform(x => new CoffeeCellViewModel(x.Id, x.Name, x.Species, x.Regions, x.Image))
                .Sort(SortExpressionComparer<CoffeeCellViewModel>.Ascending(p => p.Name))
                .Bind(out _coffeeList)
                .DisposeMany()
                .Subscribe()
                .DisposeWith(ViewModelSubscriptions);

            CoffeeDetails = ReactiveCommand.CreateFromObservable<CoffeeCellViewModel, Unit>(ExecuteNavigate).DisposeWith(ViewModelSubscriptions);

        }

        private async Task ExecuteRefresh() => await _coffeeService.Read();

        public ReactiveCommand<CoffeeCellViewModel, Unit> CoffeeDetails { get; set; }

        public ReactiveCommandBase<Unit, Unit> Refresh { get; set; }

        public ReadOnlyObservableCollection<CoffeeCellViewModel> Coffee => _coffeeList;

        protected override async Task ExecuteInitializeData() => await _coffeeService.Read();

        private IObservable<Unit> ExecuteNavigate(CoffeeCellViewModel viewModel) =>
            _viewStackService.PushPage<CoffeeDetailViewModel>(new NavigationParameter { { "Id", viewModel.Id } });
    }
}