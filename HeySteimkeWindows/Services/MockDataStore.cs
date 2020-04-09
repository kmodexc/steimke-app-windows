using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeySteimke.Models;

namespace HeySteimke.Services
{
    class MockDataStore// : IDataStore<Item,Person>
    {
        readonly List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().GetHashCode(), Name = "First item", Desc="This is an item description." },
                new Item { Id = Guid.NewGuid().GetHashCode(), Name = "Second item", Desc="This is an item description." },
                new Item { Id = Guid.NewGuid().GetHashCode(), Name = "Third item", Desc="This is an item description." },
                new Item { Id = Guid.NewGuid().GetHashCode(), Name = "Fourth item", Desc="This is an item description." },
                new Item { Id = Guid.NewGuid().GetHashCode(), Name = "Fifth item", Desc="This is an item description." },
                new Item { Id = Guid.NewGuid().GetHashCode(), Name = "Sixth item", Desc="This is an item description." }
            };
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

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public Task<Person> GetUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Person> GetProfileAsync()
        {
            throw new NotImplementedException();
        }

        public Task SetProfileAsync(Person p)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Person>> GetUsersAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(Person p)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Item>> QueryItemsAsync(int placeId)
        {
            throw new NotImplementedException();
        }
    }
}