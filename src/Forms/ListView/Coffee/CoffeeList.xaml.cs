using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Xamarin.Forms;

namespace Showroom.ListView
{
    public partial class CoffeeList : ContentPageBase<CoffeeListViewModel>
    {
        public CoffeeList()
        {
            InitializeComponent();

            this.WhenAnyValue(x => x.ViewModel.InitializeData)
                .WhereNotNull()
                .Select(x => Unit.Default)
                .InvokeCommand(this, x => x.ViewModel.InitializeData)
                .DisposeWith(ControlBindings);

            this.WhenAnyValue(x => x.ViewModel.Coffee)
                .WhereNotNull()
                .BindTo(this, x => x.CoffeeListView.ItemsSource)
                .DisposeWith(ControlBindings);

            CoffeeListView
                .Events()
                .ItemTapped
                .Select(itemTapped => itemTapped.Item as CoffeeCellViewModel)
                .InvokeCommand(this, x => x.ViewModel.CoffeeDetails)
                .DisposeWith(ControlBindings);

            CoffeeListView
                .Events()
                .ItemSelected
                .Select(itemTapped => itemTapped.SelectedItem)
                .Subscribe(selectedItem => selectedItem = null)
                .DisposeWith(ControlBindings);
        }
    }
}