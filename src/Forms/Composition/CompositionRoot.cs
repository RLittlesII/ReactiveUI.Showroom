using System;
using ReactiveUI;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using Rocket.Surgery.Airframe.Synthetic;
using Serilog;
using Sextant;
using Sextant.XamForms;
using Showroom.ListView;
using Showroom.Main;
using Showroom.Navigation;
using Showroom.Search;
using Showroom.ValueConverters;
using Splat;
using Splat.Serilog;
using Xamarin.Forms;
using CoffeeClientMock = Showroom.ListView.CoffeeClientMock;

namespace Showroom.Composition
{
    public class CompositionRoot
    {
        // TODO: [rlittlesii: July 03, 2020] Move more towards pure DI.
        public CompositionRoot(IPlatformRegistrar registrar)
        {
            
            RxApp.DefaultExceptionHandler = new ShowroomExceptionHandler();
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
            mutableDependencyResolver.RegisterView<ListOptions, ListOptionsViewModel>();
            mutableDependencyResolver.RegisterView<SearchList, SearchListViewModel>();
            mutableDependencyResolver.RegisterView<NewItem, NewItemViewModel>();
        }

        private static void RegisterViewModels(IMutableDependencyResolver mutableDependencyResolver)
        {
            mutableDependencyResolver.RegisterViewModel<MainViewModel>();
            mutableDependencyResolver.RegisterViewModel<NavigationRootViewModel>();
            mutableDependencyResolver.RegisterViewModel<CoffeeListViewModel>();
            mutableDependencyResolver.RegisterViewModel<CoffeeDetailViewModel>();
            mutableDependencyResolver.RegisterViewModel<CollectionViewModel>();
            mutableDependencyResolver.RegisterViewModel<ListOptionsViewModel>();
            mutableDependencyResolver.RegisterViewModel<SearchListViewModel>();
        }

        private static void RegisterServices(IMutableDependencyResolver mutableDependencyResolver)
        {
            mutableDependencyResolver.RegisterLazySingleton<ICoffeeService>(() => new CoffeeService(new CoffeeClientMock()));
            mutableDependencyResolver.RegisterLazySingleton<IDrinkService>(() => new DrinkDataService(new DrinkClientMock()));
            mutableDependencyResolver.RegisterLazySingleton<IPopupNavigation>(() => PopupNavigation.Instance);

            // https://reactiveui.net/docs/handbook/data-binding/value-converters#registration
            // mutableDependencyResolver.RegisterConstant(new CamelCaseSplitConverter(), typeof(IBindingTypeConverter));
        }
    }
}