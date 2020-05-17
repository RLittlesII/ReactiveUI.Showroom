using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.XamForms;
using Sextant.XamForms;
using Splat;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Showroom.Navigation
{
    public partial class NavigationRoot : ReactiveMasterDetailPage<NavigationRootViewModel>
    {
        private readonly CompositeDisposable _masterDetailBindings = new CompositeDisposable();

        public NavigationRoot()
        {
            InitializeComponent();

            Detail = Locator.Current.GetNavigationView("NavigationView");

            Events
                .DeviceDisplayMainDisplayInfoChanged
                .Where(x => x.DisplayInfo.Orientation == DisplayOrientation.Landscape && Device.Idiom == TargetIdiom.Tablet)
                .Subscribe(x => MasterBehavior = MasterBehavior.SplitOnLandscape)
                .DisposeWith(_masterDetailBindings);

            this.WhenAnyValue(x => x.ViewModel.NavigationItems)
                .Where(x => x != null)
                .BindTo(this, x => x.Menu.ItemsSource)
                .DisposeWith(_masterDetailBindings);

            Menu
                .Events()
                .ItemTapped
                .Select(x => x.Item as NavigationItemViewModel)
                .InvokeCommand(this, x => x.ViewModel.Navigate)
                .DisposeWith(_masterDetailBindings);

            Menu
                .Events()
                .ItemSelected
                .Where(x => x != null)
                .Subscribe(_ =>
                {
                    Menu.SelectedItem = null;
                    IsPresented = false;
                })
                .DisposeWith(_masterDetailBindings);

            this.Events()
                .IsPresentedChanged
                .Subscribe()
                .DisposeWith(_masterDetailBindings);

            ViewModel = new NavigationRootViewModel();
        }
    }
}