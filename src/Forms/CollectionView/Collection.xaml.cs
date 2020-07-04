using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Showroom
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Collection : ContentPageBase<CollectionViewModel>
    {
        public Collection()
        {
            InitializeComponent();
        }
    }
}