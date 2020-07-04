using System;

namespace Showroom.Composition
{
    public class ShowroomExceptionHandler : IObserver<Exception>
    {
        public void OnCompleted() { }

        public void OnError(Exception error) { }

        public void OnNext(Exception value) { }
    }
}