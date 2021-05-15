using System.Reactive.Disposables;
using ReactiveUI;
using Showroom.Extensions;

namespace Showroom
{
    public partial class OptionViewCell : ViewCellBase<OptionViewModel>
    {
        public OptionViewCell()
        {
            InitializeComponent();

            this.OneWayBind(ViewModel, x => x.Option, x => x.Option.Text, option => option.ToString().SplitCamelCase())
                .DisposeWith(ViewCellBindings);
        }
    }
}