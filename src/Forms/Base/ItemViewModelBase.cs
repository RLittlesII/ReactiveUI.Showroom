using ReactiveUI;

namespace Showroom
{
    public abstract class ItemViewModelBase : ReactiveObject
    {
        public virtual string Id { get; set; }
    }
}