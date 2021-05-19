using Sextant.XamForms;
using Showroom.Composition;
using Showroom.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Showroom
{
    public partial class App : Application
    {
        public App(IPlatformRegistrar platformRegistrar)
        {
            InitializeComponent();

            var composition = new CompositionRoot(platformRegistrar);
            MainPage = (NavigationView) composition.StartPage<NavigationRootViewModel>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}