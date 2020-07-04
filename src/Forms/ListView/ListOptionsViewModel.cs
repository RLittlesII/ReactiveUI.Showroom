using System;
using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using Sextant;
using Showroom.ListView;
using Showroom.Search;
using Splat;

namespace Showroom
{
    public class ListOptionsViewModel : ViewModelBase
    {
        private readonly IParameterViewStackService _viewStackService;

        public ListOptionsViewModel()
        {
            _viewStackService = Locator.Current.GetService<IParameterViewStackService>();

            Navigate = ReactiveCommand.CreateFromObservable<OptionViewModel, Unit>(ExecuteNavigate);

            Items = new ObservableCollection<OptionViewModel>
            {
                new OptionViewModel{ Option = ListOption.DetailNavigation },
                new OptionViewModel{ Option = ListOption.Search }
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
                    return _viewStackService.PushPage<SearchListViewModel>();
                case ListOption.DetailNavigation:
                    return _viewStackService.PushPage<CoffeeListViewModel>();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}