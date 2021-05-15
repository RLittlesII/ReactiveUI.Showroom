using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Xamarin.Forms;

namespace Showroom.CollectionView
{
    public partial class DrinkCollection
    {
        public DrinkCollection()
        {
            InitializeComponent();

            this.WhenAnyValue(x => x.ViewModel.Coffee)
                .Where(x => x != null)
                .ObserveOn(RxApp.MainThreadScheduler)
                .BindTo(this ,x => x.Drinks.ItemsSource)
                .DisposeWith(PageBindings);
            
            this.WhenAnyValue(x => x.ViewModel.Initialize)
                .Where(x => x != null)
                .Select(x => Unit.Default)
                .ObserveOn(RxApp.MainThreadScheduler)
                .InvokeCommand(this, x => x.ViewModel.Initialize)
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