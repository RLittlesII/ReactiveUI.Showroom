using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Showroom.Navigation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationItemCell : ViewCellBase<NavigationItemViewModel>
    {
        public NavigationItemCell()
        {
            InitializeComponent();

            this.Events().Tapped.Subscribe(_ =>
                {
                    

                });
        }
    }
}