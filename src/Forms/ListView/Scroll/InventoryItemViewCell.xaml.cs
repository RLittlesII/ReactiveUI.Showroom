using ReactiveUI;

namespace Showroom.Scroll
{
    public partial class InventoryItemViewCell
    {
        public InventoryItemViewCell()
        {
            InitializeComponent();

            this.OneWayBind(ViewModel, x => x.Brand, x => x.Title.Text);
            this.OneWayBind(ViewModel, x => x.Coffee, x => x.Description.Text);
        }
    }
}