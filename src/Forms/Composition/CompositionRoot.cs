using System;
using ReactiveUI;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using Rocket.Surgery.Airframe.Synthetic;
using Serilog;
using Sextant;
using Sextant.XamForms;
using Showroom.Base;
using Showroom.Coffee;
using Showroom.CollectionView;
using Showroom.CollectionView.Scroll;
using Showroom.Navigation;
using Showroom.Scroll;
using Showroom.Search;
using Showroom.ValueConverters;
using Splat;
using Splat.Serilog;
using Xamarin.Forms;
using CoffeeClientMock = Showroom.Coffee.CoffeeClientMock;

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
            RegisterViewModels(Locator.GetLocator());
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
            mutableDependencyResolver.RegisterView<CollectionView.DrinkCollection, DrinkCollectionViewModel>();
            mutableDependencyResolver.RegisterView<ListOptions, ListOptionsViewModel>();
            mutableDependencyResolver.RegisterView<CollectionView.CollectionOptions, CollectionOptionsViewModel>();
            mutableDependencyResolver.RegisterView<InfiniteScroll, InfiniteScrollViewModel>();
            mutableDependencyResolver.RegisterView<SearchList,SearchListViewModel>();
            mutableDependencyResolver.RegisterView<NewItem, NewItemViewModel>();
            mutableDependencyResolver.RegisterView<SearchCollectionView, SearchCollectionViewModel>();
            mutableDependencyResolver.RegisterView<InfiniteCollection, InfiniteCollectionViewModel>();
        }

        private static void RegisterViewModels(IDependencyResolver dependencyResolver)
        {
            dependencyResolver.RegisterViewModel<MainViewModel>();
            dependencyResolver.RegisterViewModel<NavigationRootViewModel>();
            dependencyResolver.RegisterViewModel<ListOptionsViewModel>();
            dependencyResolver.RegisterViewModel<CollectionOptionsViewModel>();
            dependencyResolver.RegisterViewModel(() => new CoffeeListViewModel(dependencyResolver.GetService<IParameterViewStackService>(), dependencyResolver.GetService<ICoffeeService>()));
            dependencyResolver.RegisterViewModel(() => new CoffeeDetailViewModel(dependencyResolver.GetService<ICoffeeService>()));
            dependencyResolver.RegisterViewModel<DrinkCollectionViewModel>();
            dependencyResolver.RegisterViewModel(() => new SearchListViewModel(dependencyResolver.GetService<IDrinkService>()));
            dependencyResolver.RegisterViewModel(() => new SearchCollectionViewModel(dependencyResolver.GetService<IDrinkService>()));
            dependencyResolver.RegisterViewModel<NewItemViewModel>();
            dependencyResolver.RegisterViewModel(() => new InfiniteScrollViewModel(dependencyResolver.GetService<IInventoryDataService>()));
            dependencyResolver.RegisterViewModel(() => new InfiniteCollectionViewModel(dependencyResolver.GetService<IInventoryDataService>()));
        }

        private static void RegisterServices(IMutableDependencyResolver mutableDependencyResolver)
        {
            mutableDependencyResolver.RegisterLazySingleton<ICoffeeService>(() => new CoffeeService(new CoffeeClientMock()));
            mutableDependencyResolver.RegisterLazySingleton<Rocket.Surgery.Airframe.Synthetic.IDrinkService>(() => new DrinkDataService(new DrinkClientMock()));
            mutableDependencyResolver.RegisterLazySingleton<IInventoryDataService>(() => new InventoryDataService(new CoffeeInventoryMock()));
            mutableDependencyResolver.RegisterLazySingleton<IPopupNavigation>(() => PopupNavigation.Instance);

            // https://reactiveui.net/docs/handbook/data-binding/value-converters#registration
            // mutableDependencyResolver.RegisterConstant(new CamelCaseSplitConverter(), typeof(IBindingTypeConverter));
        }
    }
}