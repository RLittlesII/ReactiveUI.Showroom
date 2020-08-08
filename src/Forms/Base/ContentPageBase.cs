using System;
using System.Reactive.Disposables;
using ReactiveUI.XamForms;
using Splat;
using Splat.ApplicationPerformanceMonitoring;

namespace Showroom.Base
{
    public abstract class ContentPageBase<T> : ReactiveContentPage<T>, IDisposable, IEnableLogger
        where T : ViewModelBase
    {
        protected CompositeDisposable PageBindings { get; } = new CompositeDisposable();
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                PageBindings?.Dispose();
            }
        }
    }
}