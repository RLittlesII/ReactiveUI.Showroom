namespace Showroom
{
    // public abstract class ViewModelBase : ReactiveObject, INavigable, IDestructible
    // {
    //     protected readonly CompositeDisposable Garbage = new CompositeDisposable();
    //
    //     protected ViewModelBase()
    //     {
    //         InitializeData = ReactiveCommand.CreateFromTask(ExecuteInitializeData);
    //     }
    //
    //     public virtual string Id { get; }
    //
    //     public virtual bool IsBusy { get; }
    //
    //     public virtual bool IsLoading { get; }
    //
    //     public ReactiveCommand<Unit, Unit> InitializeData { get; private set; }
    //
    //     public void Dispose()
    //     {
    //         Dispose(true);
    //         GC.SuppressFinalize(this);
    //     }
    //
    //     protected virtual IObservable<Unit> WhenNavigatedTo(INavigationParameter parameter) =>
    //         WhenNavigatedTo(parameter);
    //
    //     protected virtual IObservable<Unit> WhenNavigatedFrom(INavigationParameter parameter) =>
    //         WhenNavigatedFrom(parameter);
    //
    //     protected virtual IObservable<Unit> WhenNavigatingTo(INavigationParameter parameter) =>
    //         WhenNavigatingTo(parameter);
    //
    //     protected virtual void Destroy()
    //     {
    //         Garbage?.Dispose();
    //     }
    //
    //     protected virtual void Dispose(bool disposing)
    //     {
    //         if (disposing)
    //         {
    //             Garbage?.Dispose();
    //         }
    //     }
    //     protected virtual Task ExecuteInitializeData() => Task.CompletedTask;
    //
    //     IObservable<Unit> INavigated.WhenNavigatedTo(INavigationParameter parameter) =>
    //         WhenNavigatedTo(parameter);
    //
    //     IObservable<Unit> INavigated.WhenNavigatedFrom(INavigationParameter parameter) =>
    //         WhenNavigatedFrom(parameter);
    //
    //     IObservable<Unit> INavigating.WhenNavigatingTo(INavigationParameter parameter) =>
    //         WhenNavigatingTo(parameter);
    //
    //     void IDestructible.Destroy()
    //     {
    //         Destroy();
    //     }
    // }
}