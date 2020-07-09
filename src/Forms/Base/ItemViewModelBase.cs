using ReactiveUI;

namespace Showroom.Base
{
    public abstract class ItemViewModelBase : ReactiveObject
    {
        public virtual string Id { get; set; }
    }
}