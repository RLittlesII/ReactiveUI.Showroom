using ReactiveUI;

namespace Showroom.ListView
{
    public partial class ItemViewCell
    {
        public ItemViewCell()
        {
            InitializeComponent();

            this.OneWayBind(ViewModel, x => x.Title, x => x.Title.Text);
            this.OneWayBind(ViewModel, x => x.Description, x => x.Description.Text);
        }
    }
}