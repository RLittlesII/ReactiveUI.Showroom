using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Xamarin.Forms;

namespace Showroom.CollectionView
{
    public partial class CollectionOptions
    {
        public CollectionOptions()
        {
            InitializeComponent();

            var optionSelectionChanged = Options
                .Events()
                .SelectionChanged;

            optionSelectionChanged
                .Where(x => Options.SelectionMode == SelectionMode.Single)
                .Select(x => x.CurrentSelection.First())
                .Cast<CollectionOptionViewModel>()
                .InvokeCommand(this, x => x.ViewModel.Navigate)
                .DisposeWith(PageBindings);

            this.WhenAnyValue(x => x.ViewModel.Items)
                .BindTo(this, x => x.Options.ItemsSource)
                .DisposeWith(PageBindings);
        }
    }
}