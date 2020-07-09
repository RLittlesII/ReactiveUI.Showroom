using System;
using System.Reactive;
using ReactiveUI;
using Rocket.Surgery.Airframe.Synthetic;
using Showroom.Base;

namespace Showroom.Search
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

        public Guid Id
        {
            get => _id;
            set => this.RaiseAndSetIfChanged(ref _id, value);
        }

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