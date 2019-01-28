using System;
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
        public string report;

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
            MessagingCenter.Subscribe<NewItemPage, Item>(this, "EditItem", async (obj, item) =>
            {
                var newItem = item as Item;

                AllItems.First(x => x.Id == item.Id);
                AllItems.Remove(newItem);
                AllItems.Add(item);

                await DataStore.UpdateItemAsync(newItem);

                FilterItems();

            });
            MessagingCenter.Subscribe<NewItemPage, Item>(this, "DeleteItem", async (obj, item) =>
            {
                var newItem = item as Item;
                AllItems.Remove(newItem);
                await DataStore.DeleteItemAsync(item.Id);
                FilterItems();
            });

        }

        public string Report
        {
            get => report;
            set
            {
                if (SetProperty(ref report, value))
                        OnPropertyChanged("Report");
            }
            
        }

        private void CalculateReport()
        {
            double price = 0;
            if(Items.Count>0)
                price = Items.Sum(x => x.Price);

            Report = String.Format("Termini:{0}, Ukupno: {1} din.", Items.Count, price);
        }

        public DateTime SelectedFilter
        {
            get => selectedFilter;
            set
            {
                if (SetProperty(ref selectedFilter, value))
                    FilterItems();
            }
        }
    
        private ObservableRangeCollection<Item> UpdateAllItems(ObservableRangeCollection<Item> allItems,Item item)
        {
            
            Item tmpItem = allItems.First(x => x.Id == item.Id);
            allItems.Remove(tmpItem);
            allItems.Add(item);
            return new ObservableRangeCollection<Item>();

        }

        private void AddFilterOptions(Item newItem)
        {
            if (!FilterOptions.Contains(newItem.Date.Date))
                FilterOptions.Add(newItem.Date.Date);
        }

        private void FilterItems()
        {
            Items.ReplaceRange(AllItems.OrderBy(y=>y.Date).ThenBy(x=>x.StartTime).Where(a => a.Date.Date == SelectedFilter.Date || SelectedFilter == DateTime.MinValue));
            CalculateReport();
        }

        public Command LoadItemsCommand { get; set; }

        private async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                AllItems.Clear();
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