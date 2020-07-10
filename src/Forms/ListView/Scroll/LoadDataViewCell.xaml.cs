using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using DynamicData.Binding;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Showroom.Scroll
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadDataViewCell : ViewCellBase<LoadDataItemViewModel>
    {
        public LoadDataViewCell()
        {
            InitializeComponent();

            this.OneWayBind(ViewModel, x => x.Name, x => x.Customer.Text);

            this.OneWayBind(ViewModel, x => x.Ordered, x => x.Drink.Text);

            this.OneWayBind(ViewModel, x => x.Size, x => x.Size.Text);
        }
    }
}