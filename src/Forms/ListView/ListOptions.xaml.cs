using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Xamarin.Forms;

namespace Showroom
{
    public partial class ListOptions : ContentPageBase<ListOptionsViewModel>
    {
        public ListOptions()
        {
            InitializeComponent();

            Options
                .Events()
                .ItemTapped
                .Select(x => x.Item as OptionViewModel)
                .InvokeCommand(this, x => x.ViewModel.Navigate)
                .DisposeWith(PageBindings);

            Options
                .Events()
                .ItemSelected
                .Subscribe(item =>
                {
                    Options.SelectedItem = null;
                })
                .DisposeWith(PageBindings);

            this.WhenAnyValue(x => x.ViewModel.Items)
                .BindTo(this, x => x.Options.ItemsSource)
                .DisposeWith(PageBindings);
        }
    }
}