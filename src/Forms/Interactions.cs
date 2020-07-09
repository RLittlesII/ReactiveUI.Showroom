using System.Reactive;
using ReactiveUI;

namespace Showroom
{
    public static class Interactions
    {
        
        public static readonly Interaction<Unit, Unit> AddItem = new Interaction<Unit, Unit>(RxApp.MainThreadScheduler);
    }
}