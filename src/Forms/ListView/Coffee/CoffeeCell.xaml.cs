using System.Reactive.Disposables;
using ReactiveUI;
using Showroom.Base;
using Showroom.ValueConverters;
using Xamarin.Forms;

namespace Showroom.Coffee
{
    public partial class CoffeeCell : ViewCellBase<CoffeeCellViewModel>
    {
        public CoffeeCell()
        {
            InitializeComponent();

            this.OneWayBind(ViewModel, x => x.Name, x => x.CoffeeName.Text)
                .DisposeWith(ViewCellBindings);
            
            this.OneWayBind(ViewModel, x => x.Species, x => x.CoffeeSpecies.Text)
                .DisposeWith(ViewCellBindings);
            
            this.OneWayBind(ViewModel, x => x.Regions, x => x.CoffeeRegions.Text, vmToViewConverterOverride: new CamelCaseSplitConverter())
                .DisposeWith(ViewCellBindings);

            this.OneWayBind(ViewModel, x => x.Image, x => x.CoffeeBackground.Source, ImageSource.FromFile)
                .DisposeWith(ViewCellBindings);
        }
    }
}