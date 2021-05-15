using System;
using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using Rocket.Surgery.Airframe.ViewModels;
using Sextant;
using Sextant.Plugins.Popup;
using Showroom.ListView;
using Showroom.Navigation;
using Showroom.Scroll;
using Splat;

namespace Showroom
{
    public class ListOptionsViewModel : NavigableViewModelBase
    {
        private readonly IDetailNavigation _detailNavigation;

        public ListOptionsViewModel()
        {
            _detailNavigation = Locator.Current.GetService<IDetailNavigation>();

            Navigate = ReactiveCommand.CreateFromObservable<OptionViewModel, Unit>(ExecuteNavigate);

            Items = new ObservableCollection<OptionViewModel>
            {
                new OptionViewModel{ Option = ListOption.DetailNavigation },
                new OptionViewModel{ Option = ListOption.Search },
                new OptionViewModel{ Option = ListOption.InfiniteScroll }
            };
        }

        public ObservableCollection<OptionViewModel> Items { get; set; }

        public ReactiveCommand<OptionViewModel, Unit> Navigate { get; set; }

        private IObservable<Unit> ExecuteNavigate(OptionViewModel arg)
        {
            // HACK: [rlittlesii: July 04, 2020] Make this not suck, this is a great case for routes.
            switch (arg.Option)
            {
                case ListOption.Search:
                    return _detailNavigation.PushPage<SearchListViewModel>();
                case ListOption.DetailNavigation:
                    return _detailNavigation.PushPage<CoffeeListViewModel>();
                case ListOption.InfiniteScroll:
                    return _detailNavigation.PushPage<InfiniteScrollViewModel>();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}