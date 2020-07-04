using Xamarin.Forms.Xaml;

namespace Showroom.ListView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoffeeDetail : ContentPageBase<CoffeeDetailViewModel>
    {
        public CoffeeDetail()
        {
            InitializeComponent();
        }
    }
}