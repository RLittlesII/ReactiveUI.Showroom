using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Showroom.CollectionView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrinkView : ContentViewBase<DrinkViewModel>
    {
        public DrinkView()
        {
            InitializeComponent();

            this.OneWayBind(ViewModel, x => x.Name, x => x.CoffeeName.Text)
                .DisposeWith(ViewBindings);
            
            this.OneWayBind(ViewModel, x => x.Species, x => x.CoffeeSpecies.Text)
                .DisposeWith(ViewBindings);
            
            this.OneWayBind(ViewModel, x => x.Regions, x => x.CoffeeRegions.Text, regions => $"{string.Join(", ", regions)}".TrimEnd())
                .DisposeWith(ViewBindings);

            this.OneWayBind(ViewModel, x => x.Image, x => x.CoffeeBackground.Source, ImageSource.FromFile)
                .DisposeWith(ViewBindings);

            this.WhenAnyValue(x => x.ViewModel.Selected)
                .StartWith(false)
                .DistinctUntilChanged()
                .Where(x => x)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(_ =>
                {
                    NamePlate.Color = Color.Aqua;
                    CoffeeName.TextColor = Color.Indigo;
                });
        }
    }
}