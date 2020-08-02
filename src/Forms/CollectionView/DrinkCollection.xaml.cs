using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Showroom.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Showroom.CollectionView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrinkCollection : ContentPageBase<DrinkCollectionViewModel>
    {
        public DrinkCollection()
        {
            InitializeComponent();

            this.WhenAnyValue(x => x.ViewModel.Coffee)
                .Where(x => x != null)
                .ObserveOn(RxApp.MainThreadScheduler)
                .BindTo(this ,x => x.Drinks.ItemsSource)
                .DisposeWith(PageBindings);
            
            this.WhenAnyValue(x => x.ViewModel.InitializeData)
                .Where(x => x != null)
                .Select(x => Unit.Default)
                .ObserveOn(RxApp.MainThreadScheduler)
                .InvokeCommand(this, x => x.ViewModel.InitializeData)
                .DisposeWith(PageBindings);

            var drinkSelected = Drinks
                .Events()
                .SelectionChanged;

            drinkSelected
                .Where(_ => Drinks.SelectionMode == SelectionMode.Single)
                .Select(x => x.CurrentSelection.First())
                .Cast<DrinkViewModel>()
                .InvokeCommand(this, x => x.ViewModel.CoffeeDetails)
                .DisposeWith(PageBindings);

            var multipleSelected =
                drinkSelected
                    .Where(_ => Drinks.SelectionMode == SelectionMode.Multiple);

            multipleSelected
                .Where(args => args.PreviousSelection.Count > args.CurrentSelection.Count)
                .Subscribe(_ =>
                {
                    /* Removed Item from Selection */
                })
                .DisposeWith(PageBindings);

            multipleSelected
                .Where(args => args.PreviousSelection.Count < args.CurrentSelection.Count)
                .Subscribe(_ =>
                {
                    /* Added Item to Selection */
                })
                .DisposeWith(PageBindings);
        }
    }
}