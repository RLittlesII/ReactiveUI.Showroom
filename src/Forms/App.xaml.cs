using System;
using Showroom.Composition;
using Showroom.ListView;
using Showroom.Main;
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

            var composition = new Composition.Composition(platformRegistrar);

            MainPage = composition.StartPage<CoffeeListViewModel>();
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