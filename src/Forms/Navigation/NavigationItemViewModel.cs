using System;
using ReactiveUI;
using Showroom.Base;

namespace Showroom.Navigation
{
    public class NavigationItemViewModel : ItemViewModelBase
    {
        private string _title;

        public string Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        public string Icon { get; set; }

        public Type IViewFor { get; set; }
    }
}