using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Xamarin.Forms;

namespace Showroom.Scroll
{
    public partial class InfiniteScroll
    {
        int _start = 0;
        const int _numberOfRecords = 100;

        public InfiniteScroll()
        {
            InitializeComponent();

            this.OneWayBind(ViewModel, x => x.IsRefreshing, x => x.ListView.IsRefreshing)
                .DisposeWith(ControlBindings);

            this.OneWayBind(ViewModel, x => x.IsLoading, x => x.Activity.IsRunning)
                .DisposeWith(ControlBindings);

            this.OneWayBind(ViewModel, x => x.IsLoading, x => x.Activity.IsVisible)
                .DisposeWith(ControlBindings);

            this.Bind(ViewModel, x => x.SearchText, x => x.Search.Text)
                .DisposeWith(ControlBindings);

            this.WhenAnyValue(x => x.ViewModel.Items)
                .Where(x => x != null)
                .BindTo(this, x => x.ListView.ItemsSource)
                .DisposeWith(ControlBindings);

            ListView
                .Events()
                .Refreshing
                .Select(x => ((IList<InventoryItemViewModel>) ListView.ItemsSource).Count)
                .InvokeCommand(this, x => x.ViewModel.Load)
                .DisposeWith(ControlBindings);

            ListView
                .Events()
                .ItemSelected
                .Subscribe(item => { ListView.SelectedItem = null; })
                .DisposeWith(ControlBindings);

            var itemAppearing =
                ListView
                    .Events()
                    .ItemAppearing;

            itemAppearing
                .Throttle(TimeSpan.FromMilliseconds(300), RxApp.TaskpoolScheduler)
                .Where(x => (x.Item as InventoryItemViewModel)?.Id == ((IList<InventoryItemViewModel>) ListView.ItemsSource)?.Last()?.Id)
                .Select(x => x.ItemIndex)
                .ObserveOn(RxApp.MainThreadScheduler)
                .InvokeCommand(this, x => x.ViewModel.Load)
                .DisposeWith(ControlBindings);

            Load
                .Events()
                .Pressed
                .Select(x => ((IList) ListView.ItemsSource).Count)
                .ObserveOn(RxApp.MainThreadScheduler)
                .InvokeCommand(this, x => x.ViewModel.Load)
                .DisposeWith(ControlBindings);

            #region Original

            /* This is the original example for Item Appearing and Infinite Scroll
             * Taken from https://github.com/RobGibbens/InfiniteScrolling/blob/master/InfiniteScrolling/MainPage.xaml.cs
             */
            //     this.ListView.ItemAppearing += async (sender, e) =>
            //     {
            //         var itemViewModel = (InventoryItemViewModel) e.Item;
            //
            //         this.Log().Debug("stuff about the item");
            //         if (!ViewModel.IsLoading)
            //         {
            //
            //             if (itemViewModel.Name == ViewModel.Items.Last().Name)
            //             {
            //                 await LoadData();
            //             }
            //         }
            //     };
            // }
            //
            // async Task LoadData ()
            // {
            //     await ViewModel.LoadData (_start, _numberOfRecords);
            //     _start = _start + _numberOfRecords;
            // }

            #endregion
        }
    }
}