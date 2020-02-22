using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Showroom.Base;
using Xamarin.Forms;
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