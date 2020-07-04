using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using Rocket.Surgery.Airframe.Popup;
using Xamarin.Forms;

namespace Showroom.Search
{
    public partial class NewItem : PopupPageBase<NewItemViewModel>
    {
        public NewItem()
        {
            InitializeComponent();

            // TODO: [rlittlesii: July 04, 2020] fix once Sextant.Plugins.Popup is completed.
            ViewModel = new NewItemViewModel();

            Save
                .Events()
                .Pressed
                .Select(x => Unit.Default)
                .InvokeCommand(this, x => x.ViewModel.Save);

            Cancel
                .Events()
                .Pressed
                .Select(x => Unit.Default)
                .InvokeCommand(this, x => x.ViewModel.Cancel);
        }
    }
}