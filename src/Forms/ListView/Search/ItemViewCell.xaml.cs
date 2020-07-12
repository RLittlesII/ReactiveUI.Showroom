using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms.Xaml;

namespace Showroom.Search
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemViewCell
    {
        public ItemViewCell()
        {
            InitializeComponent();

            this.OneWayBind(ViewModel, x => x.Title, x => x.Title.Text);
            this.OneWayBind(ViewModel, x => x.Description, x => x.Description.Text);
        }
    }
}