using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using ReactiveUI;
using Rg.Plugins.Popup.Services;
using Showroom.Base;
using Splat;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Showroom.Search
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchList : ContentPageBase<SearchListViewModel>
    {
        public SearchList()
        {
            InitializeComponent();

            this.OneWayBind(ViewModel, x => x.IsRefreshing, x => x.ListView.IsRefreshing)
                .DisposeWith(ControlBindings);

            this.Bind(ViewModel, x => x.SearchText, x => x.Search.Text)
                .DisposeWith(ControlBindings);

            ListView
                .Events()
                .Refreshing
                .InvokeCommand(this, x => x.ViewModel.Refresh)
                .DisposeWith(ControlBindings);

            ListView
                .Events()
                .ItemSelected
                .Subscribe(item =>
                {
                    ListView.SelectedItem = null;
                })
                .DisposeWith(ControlBindings);

            Add.Events()
                .Pressed
                .InvokeCommand(this, x => x.ViewModel.Add)
                .DisposeWith(ControlBindings);

            this.WhenAnyValue(x => x.ViewModel.Items)
                .Where(x => x != null)
                .BindTo(this, x => x.ListView.ItemsSource)
                .DisposeWith(ControlBindings);

            Interactions
                .AddItem
                .RegisterHandler(context =>
                {
                    // HACK: [rlittlesii: July 03, 2020]
                    // This is why "service location is an anti-pattern".
                    // Because it allows developers to implement bad patterns.
                    // Service Location is a tool that can be abused, not a pattern!
                    // HACK: [rlittlesii: July 03, 2020]
                    NewItem confirmationPage = (NewItem)Locator.Current.GetService<IViewFor<NewItemViewModel>>();

                    PopupNavigation
                        .Instance
                        .PushAsync(confirmationPage)
                        .ToObservable()
                        .ObserveOn(RxApp.MainThreadScheduler)
                        .ForkJoin(
                            confirmationPage
                                .Events()
                                .Disappearing
                                .Take(1)
                                .Select(x => Unit.Default),
                            (_, __) => __)
                        .Subscribe(result =>
                        {
                            context.SetOutput(Unit.Default);
                        });

                })
                .DisposeWith(ControlBindings);
        }
    }
}