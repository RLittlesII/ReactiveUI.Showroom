using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Showroom.Base;
using Xamarin.Forms;

namespace Showroom.Coffee
{
    public partial class CoffeeList : ContentPageBase<CoffeeListViewModel>
    {
        public CoffeeList()
        {
            InitializeComponent();

            this.WhenAnyValue(x => x.ViewModel.InitializeData)
                .Where(x => x != null)
                .Select(x => Unit.Default)
                .InvokeCommand(this, x => x.ViewModel.InitializeData)
                .DisposeWith(PageBindings);

            this.WhenAnyValue(x => x.ViewModel.Coffee)
                .Where(x => x != null)
                .BindTo(this, x => x.CoffeeListView.ItemsSource)
                .DisposeWith(PageBindings);

            CoffeeListView
                .Events()
                .ItemTapped
                .Select(itemTapped => itemTapped.Item as CoffeeCellViewModel)
                .InvokeCommand(this, x => x.ViewModel.CoffeeDetails)
                .DisposeWith(PageBindings);

            CoffeeListView
                .Events()
                .ItemSelected
                .Select(itemTapped => itemTapped.SelectedItem)
                .Subscribe(selectedItem => selectedItem = null)
                .DisposeWith(PageBindings);

            CoffeeListView
                .Events()
                .ItemAppearing
                .Subscribe();

            CoffeeListView
                .Events()
                .Refreshing
                .InvokeCommand(this, x => x.ViewModel.Refresh);
        }
    }
}