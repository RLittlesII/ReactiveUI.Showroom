using System.Reactive;
using ReactiveUI;

namespace DynamicList
{
    public static class Interactions
    {
        
        public static readonly Interaction<Unit, Unit> AddItem = new Interaction<Unit, Unit>();
    }
}