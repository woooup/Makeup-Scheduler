using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using MakeupScheduler.Models;
using MakeupScheduler.Views;
using MakeupScheduler.Helpers;
using System.Linq;

namespace MakeupScheduler.ViewModels
{

    public class ItemsViewModel : BaseViewModel
    {
        private DateTime selectedFilter = DateTime.MinValue;

        public ObservableRangeCollection<Item> Items { get; set; }
        public ObservableRangeCollection<Item> AllItems { get; set; }
        public ObservableRangeCollection<DateTime> FilterOptions { get; }
        public DateTime SelectedFilter
        {
            get => selectedFilter;
            set
            {
                if (SetProperty(ref selectedFilter, value))
                    FilterItems();
            }
        }

        public ItemsViewModel()
        {
            Items = new ObservableRangeCollection<Item>();
            AllItems = new ObservableRangeCollection<Item>();

            FilterOptions = new ObservableRangeCollection<DateTime>
            {
                DateTime.MinValue
            };
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                AllItems.Add(newItem);
                AddFilterOptions(newItem);
                FilterItems();

                await DataStore.AddItemAsync(newItem);
            });
        }

      

        public Command LoadItemsCommand { get; set; }

        private void AddFilterOptions(Item newItem)
        {
            if (!FilterOptions.Contains(newItem.Date.Date))
                FilterOptions.Add(newItem.Date.Date);
        }
        private void FilterItems()
        {
            Items.ReplaceRange(AllItems.OrderBy(y=>y.Date).ThenBy(x=>x.StartTime).Where(a => a.Date.Date == SelectedFilter.Date || SelectedFilter == DateTime.MinValue));
        }
        private async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var items = await DataStore.GetItemsAsync(true);
                AllItems.ReplaceRange(items);
                FilterItems();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}