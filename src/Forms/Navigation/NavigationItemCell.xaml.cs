using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
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