using System;
using ReactiveUI;
using Serilog;
using Sextant;
using Sextant.XamForms;
using Showroom.Base;
using Showroom.ListView;
using Showroom.Main;
using Showroom.Navigation;
using Showroom.ValueConverters;
using Splat;
using Splat.Serilog;
using Xamarin.Forms;

namespace Showroom.Composition
{
    public class CompositionRoot
    {
        public CompositionRoot(IPlatformRegistrar registrar)
        {
            Locator.CurrentMutable.InitializeReactiveUI();
            Sextant.Sextant.Instance.InitializeForms();

            Locator
                .CurrentMutable
                .RegisterPlatform(registrar);

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
            mutableDependencyResolver.RegisterView<NavigationRoot, NavigationRootViewModel>();
            mutableDependencyResolver.RegisterView<CoffeeList, CoffeeListViewModel>();
            mutableDependencyResolver.RegisterView<CoffeeDetail, CoffeeDetailViewModel>();
            mutableDependencyResolver.RegisterView<Collection, CollectionViewModel>();
        }

        private static void RegisterViewModels(IMutableDependencyResolver mutableDependencyResolver)
        {
            mutableDependencyResolver.RegisterViewModel<MainViewModel>();
            mutableDependencyResolver.RegisterViewModel<NavigationRootViewModel>();
            mutableDependencyResolver.RegisterViewModel<CoffeeListViewModel>();
            mutableDependencyResolver.RegisterViewModel<CoffeeDetailViewModel>();
            mutableDependencyResolver.RegisterViewModel<CollectionViewModel>();
        }

        private static void RegisterServices(IMutableDependencyResolver mutableDependencyResolver)
        {
            mutableDependencyResolver.RegisterLazySingleton<ICoffeeService>(() => new CoffeeService(new CoffeeClientMock()));

            // https://reactiveui.net/docs/handbook/data-binding/value-converters#registration
            // mutableDependencyResolver.RegisterConstant(new CamelCaseSplitConverter(), typeof(IBindingTypeConverter));
        }
    }
}