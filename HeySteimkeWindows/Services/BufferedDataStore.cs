using HeySteimke.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HeySteimke.Services
{
    class BufferedDataStore
    {
        NetworkDataStore networkDataStore;
        LocalDataStore localDataStore;
        IList<Item> items;
        IList<Person> users;
        IList<Place> places;
        Person profile;

        public BufferedDataStore()
        {
            networkDataStore = new NetworkDataStore();
            localDataStore = new LocalDataStore();
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            if (items == null)
            {
                items = new List<Item>(await networkDataStore.GetItemsAsync());
            }
            items.Add(item);
            return await networkDataStore.AddItemAsync(item);
        }

        public async Task AddPlaceAsync(Place p)
        {
            if(places == null)
            {
                places = await networkDataStore.GetPlacesAsync();
            }
            places.Add(p);
            await networkDataStore.AddPlaceAsync(p);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            if (items == null)
            {
                items = new List<Item>(await networkDataStore.GetItemsAsync());
            }
            for(int cnt=0;cnt<items.Count;cnt++)
            {
                if (items[cnt].Id == id)
                    items.RemoveAt(cnt);
            }
            return await networkDataStore.DeleteItemAsync(id);
        }

        public async Task<Place> GetPlaceAsync(int id)
        {
            if (places == null)
            {
                places = await networkDataStore.GetPlacesAsync();
            }
            foreach (var it in places)
            {
                if (it.Id == id)
                    return it;
            }
            return new Place();
        }

        public async Task<Item> GetItemAsync(int id)
        {
            if(items == null)
            {
                items = new List<Item>(await networkDataStore.GetItemsAsync());
            }
            foreach(var it in items)
            {
                if (it.Id == id)
                    return it;
            }
            return new Item();
        }

        public async Task<IEnumerable<Place>> GetPlacesAsync(bool forceRefresh)
        {
            if (places == null || forceRefresh)
            {
                places = await networkDataStore.GetPlacesAsync();
            }
            return places;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            if (items == null || forceRefresh)
            {
                items = new List<Item>(await networkDataStore.GetItemsAsync());
            }
            return items;
        }

        public async Task AddUserAsync(Person p)
        {
            if(users == null)
            {
                users = await networkDataStore.GetUsersAsync();
            }
            users.Add(p);
            await networkDataStore.AddUserAsync(p);
        }

        public async Task<Person> GetProfileAsync()
        {
            if(profile == null)
            {
                profile = await localDataStore.GetProfileAsync();
            }
            return profile;
        }

        public async Task<Person> GetUserAsync(int id)
        {
            if(users == null)
            {
                users = await networkDataStore.GetUsersAsync();
            }
            foreach (var it in users)
            {
                if (it.Id == id)
                    return it;
            }
            return new Person();
        }

        public async Task<IEnumerable<Person>> GetUsersAsync(bool forceRefresh = false)
        {
            if (users == null)
            {
                users = await networkDataStore.GetUsersAsync();
            }
            return users;
        }

        public async Task<IEnumerable<Item>> QueryItemsAsync(int placeId)
        {
            if (items == null)
            {
                items = new List<Item>(await networkDataStore.GetItemsAsync());
            }
            IList<Item> query = new List<Item>();
            foreach (var it in items)
            {
                if (it.Placeid == placeId)
                    query.Add(it);
            }
            return query;
        }

        public async Task SetProfileAsync(Person p)
        {
            profile = p;
            await localDataStore.SetProfileAsync(p);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            if (items == null)
            {
                items = new List<Item>(await networkDataStore.GetItemsAsync());
            }
            for (int cnt = 0; cnt < items.Count; cnt++)
            {
                if (items[cnt].Id == item.Id)
                {
                    items.Remove(items[cnt]);
                    break;
                }
            }
            items.Add(item);
            return await networkDataStore.UpdateItemAsync(item);
        }

        public async Task UpdateUserAsync(Person p)
        {
            if (users == null)
            {
                users = await networkDataStore.GetUsersAsync();
            }
            for (int cnt = 0; cnt < users.Count; cnt++)
            {
                if (users[cnt].Id == p.Id)
                {
                    users.Remove(users[cnt]);
                    break;
                }
            }
            users.Add(p);
            await networkDataStore.UpdateUserAsync(p);
        }
    }
}
