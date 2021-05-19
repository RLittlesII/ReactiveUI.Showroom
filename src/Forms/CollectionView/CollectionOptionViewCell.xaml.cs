using System.Reactive.Disposables;
using ReactiveUI;
using Showroom.Extensions;

namespace Showroom.CollectionView
{
    public partial class CollectionOptionViewCell : ContentViewBase<CollectionOptionViewModel>
    {
        public CollectionOptionViewCell()
        {
            InitializeComponent();

            this.OneWayBind(ViewModel, x => x.Option, x => x.Option.Text, option => option.ToString().SplitCamelCase())
                .DisposeWith(ViewBindings);
        }
    }
}