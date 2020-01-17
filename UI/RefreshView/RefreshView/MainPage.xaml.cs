using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;

namespace RefreshViewDemo
{
    public partial class MainPage : ReactiveContentPage<MainPageViewModel>
    {
        public MainPage()
        {
            InitializeComponent();

            this.BindCommand(ViewModel, x => x.RefreshCommand, x => x.RefreshView);
            this.OneWayBind(ViewModel, x => x.IsRefreshing, x => x.RefreshView.IsRefreshing);
        }
    }
}
