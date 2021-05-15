using System;
using System.Collections.Generic;
using ReactiveUI;

namespace Showroom.ListView
{
    public class CoffeeCellViewModel : ItemViewModelBase
    {
        private string _name;
        private string _species;
        private IEnumerable<string> _regions;
        private string _image;

        public CoffeeCellViewModel(Guid id, string name, string species, IEnumerable<string> regions, string image = null)
        {
            Id = id;
            Name = name;
            Species = species;
            Regions = regions;
            Image = image;
        }

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