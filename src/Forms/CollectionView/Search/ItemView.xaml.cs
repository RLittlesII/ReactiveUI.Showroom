using ReactiveUI;

namespace Showroom.CollectionView
{
    public partial class ItemView
    {
        public ItemView()
        {
            InitializeComponent();

            this.OneWayBind(ViewModel, x => x.Title, x => x.Title.Text);
            this.OneWayBind(ViewModel, x => x.Description, x => x.Description.Text);
        }
    }
}