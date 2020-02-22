using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using Sextant;
using Showroom.Base;
using Splat;

namespace Showroom.ListView
{
    public class CoffeeDetailViewModel : ViewModelBase
    {
        private readonly ICoffeeService _coffeeService;
        private Guid? _coffeeId;
        private ObservableAsPropertyHelper<CoffeeDto> _detail;
        private string _imageSource;

        public CoffeeDetailViewModel()
        {
            _coffeeService = Locator.Current.GetService<ICoffeeService>();

            GetDetail = ReactiveCommand.CreateFromObservable<Guid, Unit>(ExecuteGetDetail);

            this.WhenAnyValue(x => x.CoffeeId)
                .WhereNotNull()
                .InvokeCommand(this, x => x.GetDetail);
        }

        private IObservable<Unit> ExecuteGetDetail(Guid id) =>
            Observable
                .Create<Unit>(observer =>
                {
                    _detail = _coffeeService.Read(id).WhereNotNull().ToProperty(this, x => x.Detail);
                    return Disposable.Empty;
                });

        public ReactiveCommand<Guid, Unit> GetDetail { get; set; }

        public override IObservable<Unit> WhenNavigatingTo(INavigationParameter parameter)
        {
            if (parameter.TryGetValue("Id", out var id))
            {
                CoffeeId = id as Guid?;
            }
            return Observable.Return(Unit.Default);
        }

        public CoffeeDto Detail => _detail?.Value;

        public Guid? CoffeeId
        {
            get => _coffeeId;
            set => this.RaiseAndSetIfChanged(ref _coffeeId, value);
        }
    }
}