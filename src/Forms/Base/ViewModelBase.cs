using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using Sextant;

namespace Showroom.Base
{
    public abstract class ViewModelBase : ReactiveObject, INavigable, IDisposable
    {
        protected readonly CompositeDisposable ViewModelSubscriptions = new CompositeDisposable();

        protected ViewModelBase()
        {
            InitializeData = ReactiveCommand.CreateFromTask(ExecuteInitializeData).DisposeWith(ViewModelSubscriptions);
        }

        public virtual string Id { get; }

        public ReactiveCommand<Unit, Unit> InitializeData { get; private set; }

        public virtual IObservable<Unit> WhenNavigatedTo(INavigationParameter parameter) =>
            Observable.Return(Unit.Default);

        public virtual IObservable<Unit> WhenNavigatedFrom(INavigationParameter parameter) =>
            Observable.Return(Unit.Default);

        public virtual IObservable<Unit> WhenNavigatingTo(INavigationParameter parameter) =>
            Observable.Return(Unit.Default);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual Task ExecuteInitializeData() => Task.CompletedTask;

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                ViewModelSubscriptions?.Dispose();
            }
        }
    }
}