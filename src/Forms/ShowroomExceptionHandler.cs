using System;
using System.Diagnostics;
using System.Reactive.Concurrency;
using ReactiveUI;
using Splat;

namespace Showroom.Composition
{
    public class ShowroomExceptionHandler : IObserver<Exception>, IEnableLogger
    {
        public void OnCompleted() { }

        public void OnError(Exception error) { 
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }

            this.Log().Error(error);
            RxApp.MainThreadScheduler.Schedule(() => throw error);
        }

        public void OnNext(Exception value) { }
    }
}