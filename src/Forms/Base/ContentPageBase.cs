using ReactiveUI;
using ReactiveUI.XamForms;

namespace Showroom.Base
{
    public abstract class ContentPageBase<T> : ReactiveContentPage<T>
        where T : ViewModelBase
    {
    }
}