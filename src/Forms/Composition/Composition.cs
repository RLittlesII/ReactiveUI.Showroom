using System;
using ReactiveUI;
using Serilog;
using Sextant;
using Sextant.XamForms;
using Showroom.Base;
using Splat;
using Splat.Serilog;
using Xamarin.Forms;

namespace Showroom
{
    public class Composition
    {
        public Composition(IPlatformRegistrar registrar)
        {
            Locator.CurrentMutable.InitializeReactiveUI();
            Sextant.Sextant.Instance.InitializeForms();

            Locator
                .CurrentMutable
                .RegisterPlatform(registrar)
                .RegisterView<MainPage, MainViewModel>()
                .RegisterViewModel<MainViewModel>();

            Locator.CurrentMutable.UseSerilogFullLogger(Log.Logger);

            RegisterServices(Locator.CurrentMutable);
            RegisterViews(Locator.CurrentMutable);
            RegisterViewModels(Locator.CurrentMutable);
        }

        public Page StartPage<TViewModel>()
            where TViewModel : ViewModelBase
        {
            Locator
                .Current
                .GetService<IParameterViewStackService>()
                .PushPage<TViewModel>(resetStack: true, animate: false)
                .Subscribe();

            return Locator.Current.GetNavigationView("NavigationView");
        }

        

        private static void RegisterViews(IMutableDependencyResolver mutableDependencyResolver)
        {
            mutableDependencyResolver.RegisterView<MainPage, MainViewModel>();
        }

        private static void RegisterViewModels(IMutableDependencyResolver mutableDependencyResolver)
        {
            mutableDependencyResolver.RegisterViewModel<MainViewModel>();
        }

        private static void RegisterServices(IMutableDependencyResolver mutableDependencyResolver)
        {
        }
    }
}