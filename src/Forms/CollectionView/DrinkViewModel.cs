using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using ReactiveUI;
using Showroom.Extensions;

namespace Showroom.CollectionView
{
    public class DrinkViewModel : ItemViewModelBase
    {
        private string _name;
        private string _species;
        private Guid _id;
        private IEnumerable<string> _regions;
        private string _image;
        private bool _selected;

        public DrinkViewModel(Guid id, string name, string species, IEnumerable<string> regions, string image = null)
        {
            Id = id;
            Name = name;
            Species = species;
            Regions = regions.Select(StringExtensions.SplitCamelCase);
            Image = image;
            Toggle = ReactiveCommand.Create(() => { Selected = true; });
        }

        public bool Selected
        {
            get => _selected;
            set => this.RaiseAndSetIfChanged(ref _selected, value);
        }

        public ReactiveCommand<Unit, Unit> Toggle { get; set; }

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public string Species
        {
            get => _species;
            set => this.RaiseAndSetIfChanged(ref _species, value);
        }

        public IEnumerable<string> Regions
        {
            get => _regions;
            set => this.RaiseAndSetIfChanged(ref _regions, value);
        }

        public string Image
        {
            get => _image;
            set => this.RaiseAndSetIfChanged(ref _image, value);
        }
    }
}