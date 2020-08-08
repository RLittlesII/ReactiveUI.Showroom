using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Showroom.CollectionView.Scroll
{
    public partial class InfiniteItemView
    {
        public InfiniteItemView()
        {
            InitializeComponent();
            
            this.OneWayBind(ViewModel, x => x.Brand, x => x.Title.Text);
            this.OneWayBind(ViewModel, x => x.Coffee, x => x.Description.Text);
        }
    }
}