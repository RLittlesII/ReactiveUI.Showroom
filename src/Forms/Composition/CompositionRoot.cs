using System;
using ReactiveUI;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using Rocket.Surgery.Airframe.Synthetic;
using Rocket.Surgery.Airframe.ViewModels;
using Serilog;
using Sextant;
using Sextant.Plugins.Popup;
using Sextant.XamForms;
using Showroom.CollectionView;
using Showroom.CollectionView.Scroll;
using Showroom.ListView;
using Showroom.Navigation;
using Showroom.Scroll;
using Splat;
using Splat.Serilog;
using Xamarin.Forms;
using CoffeeClientMock = Showroom.ListView.CoffeeClientMock;
using CoffeeDetail = Showroom.ListView.CoffeeDetail;
using CoffeeList = Showroom.ListView.CoffeeList;
using SearchList = Showroom.ListView.SearchList;

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
                .RegisterPlatform(registrar)
                .RegisterNavigationView(() => new NavigationView(RxApp.MainThreadScheduler, RxApp.TaskpoolScheduler, ViewLocator.Current))
                .RegisterParameterViewStackService()
                .UseSerilogFullLogger(Log.Logger);

            RegisterServices(Locator.GetLocator());
            RegisterViews(Locator.GetLocator());
            RegisterViewModels(Locator.GetLocator());
        }

        public Page StartPage<TViewModel>()
            where TViewModel : NavigableViewModelBase
        {
            var popupViewStackService = Locator
                .Current
                .GetService<IPopupViewStackService>();

            popupViewStackService
                .PushPage<TViewModel>(resetStack: true, animate: false)
                .Subscribe();

            return Locator.Current.GetNavigationView("NavigationView");
        }

        private static void RegisterViews(IDependencyResolver dependencyResolver)
        {
            dependencyResolver.RegisterLazySingleton<IDetailView>(() => new DetailView());
            dependencyResolver.RegisterView<MainPage, MainViewModel>();
            dependencyResolver.RegisterView<NavigationRoot, NavigationRootViewModel>(() => new NavigationRoot(dependencyResolver.GetService<IDetailNavigation>()));
            dependencyResolver.RegisterView<CoffeeList, CoffeeListViewModel>();
            dependencyResolver.RegisterView<CoffeeDetail, CoffeeDetailViewModel>();
            dependencyResolver.RegisterView<CollectionView.DrinkCollection, DrinkCollectionViewModel>();
            dependencyResolver.RegisterView<ListOptions, ListOptionsViewModel>();
            dependencyResolver.RegisterView<CollectionView.CollectionOptions, CollectionOptionsViewModel>();
            dependencyResolver.RegisterView<InfiniteScroll, InfiniteScrollViewModel>();
            dependencyResolver.RegisterView<SearchList,SearchListViewModel>();
            dependencyResolver.RegisterView<ListView.NewItem, NewItemViewModel>();
            dependencyResolver.RegisterView<SearchCollectionView, SearchCollectionViewModel>();
            dependencyResolver.RegisterView<InfiniteCollection, InfiniteCollectionViewModel>();
        }

        private static void RegisterViewModels(IDependencyResolver dependencyResolver)
        {
            dependencyResolver.RegisterViewModel<MainViewModel>();
            dependencyResolver.RegisterViewModel<NavigationRootViewModel>();
            dependencyResolver.RegisterViewModel<ListOptionsViewModel>();
            dependencyResolver.RegisterViewModel<CollectionOptionsViewModel>();
            dependencyResolver.RegisterViewModel(() => new CoffeeListViewModel(dependencyResolver.GetService<IPopupViewStackService>(), dependencyResolver.GetService<ICoffeeService>()));
            dependencyResolver.RegisterViewModel(() => new CoffeeDetailViewModel(dependencyResolver.GetService<ICoffeeService>()));
            dependencyResolver.RegisterViewModel<DrinkCollectionViewModel>();
            dependencyResolver.RegisterViewModel(() => new SearchListViewModel(dependencyResolver.GetService<IDrinkService>()));
            dependencyResolver.RegisterViewModel(() => new SearchCollectionViewModel(dependencyResolver.GetService<IDrinkService>()));
            dependencyResolver.RegisterViewModel<NewItemViewModel>();
            dependencyResolver.RegisterViewModel(() => new InfiniteScrollViewModel(dependencyResolver.GetService<IInventoryDataService>()));
            dependencyResolver.RegisterViewModel(() => new InfiniteCollectionViewModel(dependencyResolver.GetService<IInventoryDataService>()));
        }

        private static void RegisterServices(IDependencyResolver dependencyResolver)
        {
            dependencyResolver.RegisterLazySingleton<IPopupViewStackService>(CreatePopupNavigation(dependencyResolver));
            dependencyResolver.Register<IDetailNavigation>(CreateDetailNavigation(dependencyResolver));
            dependencyResolver.RegisterLazySingleton<ICoffeeService>(() => new CoffeeService(new CoffeeClientMock()));
            dependencyResolver.RegisterLazySingleton<IDrinkService>(() => new DrinkDataService(new DrinkClientMock()));
            dependencyResolver.RegisterLazySingleton<IInventoryDataService>(() => new InventoryDataService(new CoffeeInventoryMock()));
            dependencyResolver.RegisterLazySingleton<IPopupNavigation>(() => PopupNavigation.Instance);

            // https://reactiveui.net/docs/handbook/data-binding/value-converters#registration
            // mutableDependencyResolver.RegisterConstant(new CamelCaseSplitConverter(), typeof(IBindingTypeConverter));
        }

        private static Func<IDetailNavigation> CreateDetailNavigation(IDependencyResolver dependencyResolver)
        {
            return () => new DetailNavigation(dependencyResolver.GetService<IDetailView>(),
                dependencyResolver.GetService<IPopupNavigation>(),
                dependencyResolver.GetService<IViewLocator>(),
                dependencyResolver.GetService<IViewModelFactory>());
        }
        private static Func<IPopupViewStackService> CreatePopupNavigation(IDependencyResolver dependencyResolver)
        {
            return () => new PopupViewStackService(dependencyResolver.GetService<IView>(nameof(NavigationView)),
                dependencyResolver.GetService<IPopupNavigation>(), 
                dependencyResolver.GetService<IViewLocator>(),
                dependencyResolver.GetService<IViewModelFactory>());
        }
    }
}
