using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Showroom.Scroll
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfiniteLoad : ContentPageBase<InfiniteLoadViewModel>
    {
        public InfiniteLoad()
        {
            InitializeComponent();

            this.WhenAnyValue(x => x.ViewModel.Orders)
                .Where(x => x != null)
                .BindTo(this, x => x.LoadDataListView.ItemsSource)
                .DisposeWith(ControlBindings);

            this.WhenAnyValue(x => x.ViewModel)
                .Where(x => x != null)
                .Select(x => Unit.Default)
                .ObserveOn(RxApp.MainThreadScheduler)
                .InvokeCommand(this, x => x.ViewModel.InitializeData)
                .DisposeWith(ControlBindings);

            LoadDataListView
                .Events()
                .Refreshing
                .Select(x => ((IList<LoadDataItemViewModel>)LoadDataListView.ItemsSource).Count)
                .InvokeCommand(this, x => x.ViewModel.LoadData)
                .DisposeWith(ControlBindings);

            LoadDataListView
                .Events()
                .ItemSelected
                .Subscribe(item =>
                {
                    LoadDataListView.SelectedItem = null;
                })
                .DisposeWith(ControlBindings);
        }
    }
}