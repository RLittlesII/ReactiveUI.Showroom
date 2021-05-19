using System;
using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using Rocket.Surgery.Airframe.ViewModels;
using Sextant;
using Sextant.Plugins.Popup;
using Showroom.CollectionView;
using Showroom.Navigation;
using Splat;
using static Showroom.FontAwesomeIcons;

namespace Showroom
{
    public class NavigationMenuViewModel : NavigableViewModelBase
    {
        private readonly IPopupViewStackService _detailNavigation;
        private ReadOnlyObservableCollection<NavigationItemViewModel> _items;

        public NavigationMenuViewModel(IDetailNavigation detailNavigation)
        {
            _detailNavigation = detailNavigation;

            Items = new ReadOnlyObservableCollection<NavigationItemViewModel>(
                new ObservableCollection<NavigationItemViewModel>
                {
                    new NavigationItemViewModel
                        {Title = "List View", Icon = ListAlt, IViewFor = typeof(ListOptionsViewModel)},
                    new NavigationItemViewModel
                        {Title = "Collection View", Icon = LayerGroup, IViewFor = typeof(CollectionOptionsViewModel)}
                });

            Navigate = ReactiveCommand.CreateFromObservable<NavigationItemViewModel, Unit>(ExecuteNavigate);
        }

        public ReadOnlyObservableCollection<NavigationItemViewModel> Items
        {
            get => _items;
            set => this.RaiseAndSetIfChanged(ref _items, value);
        }

        public ReactiveCommandBase<NavigationItemViewModel,Unit> Navigate { get; }

        private IObservable<Unit> ExecuteNavigate(NavigationItemViewModel navigationItemViewModel)
        {
            var navigable = (INavigable)Locator.Current.GetService(navigationItemViewModel.IViewFor);
            return _detailNavigation.PushPage(navigable, resetStack: true);
        }
    }
}