using System;
using ReactiveUI;

namespace Showroom
{
    public abstract class ItemViewModelBase : ReactiveObject
    {
        private Guid _id;

        public Guid Id
        {
            get => _id;
            set => this.RaiseAndSetIfChanged(ref _id, value);
        }
    }
}