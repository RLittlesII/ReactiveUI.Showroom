using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Splat;
using Xamarin.Forms;

namespace Showroom.CollectionView.Scroll
{
    public partial class InfiniteCollection
    {
        public InfiniteCollection()
        {
            InitializeComponent();

            this.WhenAnyValue(x => x.ViewModel)
                .Where(x => x != null)
                .Select(x => Unit.Default)
                .InvokeCommand(this, x => x.ViewModel.Initialize)
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

            var loadPressed =
                Load
                    .Events()
                    .Pressed
                    .Select(x => Unit.Default)
                    .Do(_ => this.Log().Debug($"{nameof(Load.Pressed)}"));

            var itemThresholdReached =
                Collection
                    .Events()
                    .RemainingItemsThresholdReached
                    .Select(x => Unit.Default)
                    .Do(_ => this.Log().Debug($"{nameof(Collection.RemainingItemsThresholdReached)}"));

            itemThresholdReached
                .Merge(loadPressed)
                .Throttle(TimeSpan.FromSeconds(10), RxApp.TaskpoolScheduler)
                .Do(_ => this.Log().Debug("Merged Observable"))
                .ObserveOn(RxApp.MainThreadScheduler)
                .InvokeCommand(this, x => x.ViewModel.Load)
                .DisposeWith(PageBindings);
        }
    }
}