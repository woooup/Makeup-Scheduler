using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakeupScheduler.Models;

namespace MakeupScheduler.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>();
            var mockItems = new List<Item>
            {
                new Item { Id = Guid.NewGuid().ToString(),Name = "First item", Date = DateTime.Now,
                    StartTime =new TimeSpan(12,0,0),EndTime = new TimeSpan(13,0,0), Accessories=true, Price=1000 },

                new Item { Id = Guid.NewGuid().ToString(), Name = "Second item", Date = DateTime.Now,
                    StartTime =new TimeSpan(14,0,0),EndTime = new TimeSpan(16,0,0), Accessories=false, Price=1500 },


            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}