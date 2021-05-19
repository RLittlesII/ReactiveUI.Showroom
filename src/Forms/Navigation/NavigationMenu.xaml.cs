using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Xamarin.Forms;

namespace Showroom.Navigation
{
    public partial class NavigationMenu
    {
        private readonly CompositeDisposable Disappearing = new CompositeDisposable();
        public NavigationMenu(NavigationMenuViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;

            Menu.Events()
                .ItemTapped
                .Select(x => x.Item as NavigationItemViewModel)
                .InvokeCommand(this, x => x.ViewModel.Navigate)
                .DisposeWith(Disappearing);

            Menu.Events()
                .ItemSelected
                .Where(x => x != null)
                .Subscribe(_ => Menu.SelectedItem = null)
                .DisposeWith(Disappearing);
        }
    }
}