using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;

namespace Showroom.CollectionView
{
    public partial class SearchCollectionView
    {
        public SearchCollectionView()
        {
            InitializeComponent();

            this.Bind(ViewModel, x => x.SearchText, x => x.Search.Text)
                .DisposeWith(PageBindings);

            this.WhenAnyValue(x => x.ViewModel.Items)
                .Where(x => x != null)
                .BindTo(this, x => x.SearchCollection.ItemsSource)
                .DisposeWith(PageBindings);

            this.WhenAnyValue(x => x.ViewModel.InitializeData)
                .Where(x => x != null)
                .Select(x => Unit.Default)
                .InvokeCommand(this, x => x.ViewModel.InitializeData)
                .DisposeWith(PageBindings);
        }
    }
}