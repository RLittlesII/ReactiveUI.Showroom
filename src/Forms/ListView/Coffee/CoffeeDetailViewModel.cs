using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Rocket.Surgery.Airframe.ViewModels;

namespace Showroom.ListView
{
    public class CoffeeDetailViewModel : NavigableViewModelBase
    {
        private readonly ObservableAsPropertyHelper<CoffeeDto> _detail;

        public CoffeeDetailViewModel(ICoffeeService coffeeService)
        {
            NavigatingTo
                .Where(x => x.ContainsKey("Id"))
                .Select(x => x["Id"])
                .Cast<Guid>()
                .SelectMany(coffeeService.Read)
                .Where(x => x != null)
                .ToProperty(this, x => x.Detail, out _detail)
                .DisposeWith(Garbage);
        }

        public CoffeeDto Detail => _detail.Value;
    }
}