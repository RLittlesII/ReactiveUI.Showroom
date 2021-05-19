using System;
using System.Reactive;
using ReactiveUI;
using Rocket.Surgery.Airframe.Synthetic;

namespace Showroom.CollectionView
{
    public class ItemViewModel : ItemViewModelBase
    {
        private Guid _id;
        private string _title;
        private string _description;
        private DrinkType _type;

        public ItemViewModel()
        {
            Remove = ReactiveCommand.Create(() => { });
        }

        public ReactiveCommand<Unit, Unit> Remove { get; set; }

        public string Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        public string Description
        {
            get => _description;
            set => this.RaiseAndSetIfChanged(ref _description, value);
        }

        public DrinkType Type
        {
            get => _type;
            set => this.RaiseAndSetIfChanged(ref _type, value);
        }
    }
}