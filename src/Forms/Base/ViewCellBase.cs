using System.Reactive.Disposables;
using ReactiveUI.XamForms;

namespace Showroom.Base
{
    public abstract class ViewCellBase<T> : ReactiveViewCell<T>
        where T : ItemViewModelBase
    {
        protected CompositeDisposable ViewCellBindings { get; } = new CompositeDisposable();
    }
}