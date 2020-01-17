using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Threading.Tasks;
using ReactiveUI;

namespace RefreshViewDemo
{
    public class MainPageViewModel : ReactiveObject
    {
        const int RefreshDuration = 2;
        int itemNumber = 1;
        readonly Random random;
        private readonly ObservableAsPropertyHelper<bool> _isRefreshing;

        public MainPageViewModel()
        {
            random = new Random();
            Items = new ObservableCollection<Item>();
            AddItems();

            RefreshCommand = ReactiveCommand.CreateFromTask(async () => await RefreshItemsAsync(), outputScheduler: RxApp.MainThreadScheduler);

            _isRefreshing =
                this.WhenAnyObservable(x => x.RefreshCommand.IsExecuting)
                    .StartWith(false)
                    .DistinctUntilChanged()
                    .ToProperty(this, nameof(IsRefreshing), scheduler: RxApp.MainThreadScheduler);
        }

        public bool IsRefreshing => _isRefreshing.Value;

        public ObservableCollection<Item> Items { get; private set; }

        public ReactiveCommand<Unit, Unit> RefreshCommand { get; set; }

        void AddItems()
        {
            for (int i = 0; i < 50; i++)
            {
                Items.Add(new Item
                {
                    Color = Color.FromRgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255)),
                    Name = $"Item {itemNumber++}"
                });
            }
        }

        async Task RefreshItemsAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(RefreshDuration));
            AddItems();
        }
    }
}
