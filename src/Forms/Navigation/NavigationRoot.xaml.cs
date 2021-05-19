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
    public partial class NavigationRoot
    {
        private readonly CompositeDisposable _masterDetailBindings = new CompositeDisposable();

        public NavigationRoot(IDetailNavigation detailNavigation, IViewFor<NavigationMenuViewModel> menuPage)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            detailNavigation.PushPage<MainViewModel>(resetStack: true, animate: false).Subscribe();

            Detail = (NavigationView) detailNavigation.View;
            Master = (ContentPage) menuPage;

            menuPage
                .WhenAnyObservable(x => x.ViewModel.Navigate)
                .Select(_ => false)
                .BindTo(this, x => x.IsPresented);

            Events
                .DeviceDisplayMainDisplayInfoChanged
                .Where(x => x.DisplayInfo.Orientation == DisplayOrientation.Landscape && Device.Idiom == TargetIdiom.Tablet)
                .Subscribe(x => MasterBehavior = MasterBehavior.SplitOnLandscape)
                .DisposeWith(_masterDetailBindings);

            // HACK: [rlittlesii: July 04, 2020] This is a hack around a Xamarin.Forms iOS issue.
            this.WhenAnyValue(x => x.IsPresented)
                .Where(x => Device.RuntimePlatform == Device.iOS)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(_ => Master.IconImageSource = ImageSource.FromFile("hamburger.png"))
                .DisposeWith(_masterDetailBindings);

            ViewModel = new NavigationRootViewModel();
        }
    }
}