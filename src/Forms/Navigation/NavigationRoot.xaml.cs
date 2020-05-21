using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.XamForms;
using Sextant.XamForms;
using Splat;
using Xamarin.Essentials;
using Xamarin.Forms;

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

            this.WhenAnyValue(x => x.IsPresented)
                .Where(x => Device.RuntimePlatform == Device.iOS)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(_ => Master.IconImageSource = ImageSource.FromFile("hamburger.png"))
                .DisposeWith(_masterDetailBindings);

            ViewModel = new NavigationRootViewModel();
        }
    }
}