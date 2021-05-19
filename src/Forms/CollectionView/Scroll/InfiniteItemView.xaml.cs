using ReactiveUI;

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