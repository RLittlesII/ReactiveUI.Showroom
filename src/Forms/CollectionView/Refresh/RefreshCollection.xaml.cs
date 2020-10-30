using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Splat;
using Xamarin.Forms;

namespace Showroom.CollectionView.Refresh
{
    public partial class RefreshCollection
    {
        public RefreshCollection()
        {
            InitializeComponent();

            // this.OneWayBind(ViewModel, x => x.Refreshing, x => x.Refresh.IsRefreshing);
            // this.BindCommand(ViewModel, x => x.Refresh, x => x.Refresh);

            this.WhenAnyValue(x => x.ViewModel)
                .Where(x => x != null)
                .Select(x => Unit.Default)
                .InvokeCommand(this, x => x.ViewModel.InitializeData)
                .DisposeWith(PageBindings);

            this.WhenAnyValue(x => x.ViewModel.Items)
                .Where(x => x != null)
                .BindTo(this, x => x.Collection.ItemsSource)
                .DisposeWith(PageBindings);

            Collection
                .Events()
                .SelectionChanged
                .Subscribe(_ => Collection.SelectedItem = null)
                .DisposeWith(PageBindings);

            Load
                .Events()
                .Pressed
                .Select(x => Unit.Default)
                .Do(_ => this.Log().Debug($"{nameof(Load.Pressed)}"))
                .InvokeCommand(this, x => x.ViewModel.Load)
                .DisposeWith(PageBindings);

            Refresh
                .Events()
                .Refreshing
                .Select(_ => true)
                .CombineLatest(this.WhenAnyObservable(x => x.ViewModel.Refresh.IsExecuting), (refreshing, executing) => (refreshing, executing))
                .Select(x => x.executing)
                .BindTo(this, x => x.Refresh.IsRefreshing);

            // Refresh
            //     .Events()
            //     .Refreshing
            //     .Select(_ => Collection.ItemsSource)
            //     .Cast<IEnumerable<RefreshItemViewModel>>()
            //     .Select(x => x.Count())
            //     .InvokeCommand(this, x => x.ViewModel.Refresh)
            //     .DisposeWith(PageBindings);
        }
    }
}