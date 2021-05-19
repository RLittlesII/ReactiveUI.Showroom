using System;
using System.Reactive.Disposables;
using ReactiveUI;
using ReactiveUI.XamForms;

namespace Showroom
{
    public abstract class ContentViewBase<T> : ReactiveContentView<T>, IDisposable
        where T : class, IReactiveObject
    {
        protected CompositeDisposable ViewBindings { get; } = new CompositeDisposable();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                ViewBindings?.Dispose();
            }
        }
    }
}