using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakeupScheduler.Models;
using SQLite;
using System.IO;
using System.Diagnostics;

namespace MakeupScheduler.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        List<Item> items;
        private static MockDataStore instance = null;
        private static readonly object padlock = new object();
        string DatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "dbMakeupScheduler.db");
        
        private SQLiteConnection db;
        
        public MockDataStore()
        {
            db = new SQLiteConnection(DatabasePath);
            items = new List<Item>();
        
            try
            {
                db.CreateTable<Item>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }


        public static MockDataStore Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new MockDataStore();
                    }
                    return instance;
                }
            }
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            try
            {
                items.Add(item);
                db.Insert(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            try
            {
                var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
                items.Remove(oldItem);
                items.Add(item);
        
                db.Update(item);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
           
       
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            bool result = true;
            items.Remove(oldItem);


            if (db.Delete(oldItem) == 0)
                result = false;
            return await Task.FromResult(result);
        }

        public async Task<Item> GetItemAsync(int id)
        {
            return await Task.FromResult(db.Table<Item>().Where(i => i.Id == id).FirstOrDefault());
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(db.Table<Item>().ToList());
        }
    }
}