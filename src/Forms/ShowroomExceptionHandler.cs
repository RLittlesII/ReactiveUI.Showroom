using System;
using System.Diagnostics;
using System.Reactive.Concurrency;
using ReactiveUI;
using Splat;

namespace Showroom
{
    public class ShowroomExceptionHandler : IObserver<Exception>, IEnableLogger
    {
        /// <inheritdoc/>
        public virtual void OnNext(Exception value)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }

            this.Log().Error(value);

            RxApp.MainThreadScheduler.Schedule(() => throw value);
        }

        /// <inheritdoc/>
        public virtual void OnError(Exception error)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }

            this.Log().Error(error);

            RxApp.MainThreadScheduler.Schedule(() => throw error);
        }

        /// <inheritdoc/>
        public virtual void OnCompleted()
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }

            RxApp.MainThreadScheduler.Schedule(() => this.Log().Info($"The {nameof(ShowroomExceptionHandler)} has completed!"));
        }
    }
}