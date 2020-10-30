using ReactiveUI;

namespace Showroom.CollectionView.Refresh
{
    public partial class RefreshItemView
    {
        public RefreshItemView()
        {
            InitializeComponent();
            
            this.OneWayBind(ViewModel, x => x.Brand, x => x.Title.Text);
            this.OneWayBind(ViewModel, x => x.Coffee, x => x.Description.Text);
        }
    }
}