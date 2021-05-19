using ReactiveUI;
using Rg.Plugins.Popup.Contracts;
using Sextant;
using Sextant.Plugins.Popup;
using Sextant.XamForms;

namespace Showroom.Navigation
{
    public class DetailView : NavigationView, IDetailView {}

    public interface IDetailView : IView
    {
    }
    
    public interface IDetailNavigation : IPopupViewStackService
    {
    }
    
    public class DetailNavigation : PopupViewStackServiceBase, IDetailNavigation
    {
        public DetailNavigation(IDetailView view, IPopupNavigation popupNavigation, IViewLocator viewLocator, IViewModelFactory viewModelFactory)
            : base(view, popupNavigation, viewLocator, viewModelFactory)
        {
        }
    }
}