using HeySteimke.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HeySteimke.Services
{
    public class DataStore : IDataStore , IDataBase<Item,Person,Place>
    {
        BufferedDataStore dataStore;
        ItemsService itemsService;
        UserService userService;
        PlacesService placesService;
        public DataStore()
        {
            dataStore = new BufferedDataStore();
            itemsService = new ItemsService(this);
            userService = new UserService(this);
            placesService = new PlacesService(this);
        }
        public Task<bool> AddItemAsync(Item item)
        {
            return dataStore.AddItemAsync(item);
        }

        public Task AddPlaceAsync(Place p)
        {
            return dataStore.AddPlaceAsync(p);
        }

        public Task<bool> DeleteItemAsync(int id)
        {
            return dataStore.DeleteItemAsync(id);
        }

        public Task<Item> GetItemAsync(int id)
        {
            return dataStore.GetItemAsync(id);
        }

        public Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return dataStore.GetItemsAsync(forceRefresh);
        }

        public Task<ItemsService> GetItemsServiceAsync()
        {
            return Task.FromResult(new ItemsService(this));
        }

        public Task<Place> GetPlaceAsync(int id)
        {
            return dataStore.GetPlaceAsync(id);
        }

        public Task<IEnumerable<Place>> GetPlacesAsync(bool forceRefresh = false)
        {
            return dataStore.GetPlacesAsync(forceRefresh);
        }

        public Task<PlacesService> GetPlacesServiceAsync()
        {
            return Task.FromResult(placesService);
        }

        public Task<Person> GetProfileAsync()
        {
            return dataStore.GetProfileAsync();
        }

        public Task<Person> GetUserAsync(int id)
        {
            return dataStore.GetUserAsync(id);
        }

        public Task<IEnumerable<Person>> GetUsersAsync(bool forceRefresh = false)
        {
            return dataStore.GetUsersAsync(forceRefresh);
        }

        public Task<UserService> GetUserServiceAsync()
        {
            return Task.FromResult(userService);
        }

        public Task<IEnumerable<Item>> QueryItemsAsync(int placeId)
        {
            return dataStore.QueryItemsAsync(placeId);
        }

        public async Task SetProfileAsync(Person p)
        {
            await dataStore.SetProfileAsync(p);
            var users = await dataStore.GetUsersAsync();
            bool userExists = false;
            foreach(var user in users)
            {
                if (user.Id == p.Id)
                {
                    userExists = true;
                    break;
                }
            }
            if (userExists)
            {
                var servUsr = await dataStore.GetUserAsync(p.Id);

                // only update workload
                if (servUsr.Workload == p.Workload)
                    return;

                await dataStore.UpdateUserAsync(p);
            }
            else
            {
                await dataStore.AddUserAsync(p);
            }
        }

        public Task<bool> UpdateItemAsync(Item item)
        {
            return dataStore.UpdateItemAsync(item);
        }

        public Task UpdateUserAsync(Person p)
        {
            return dataStore.UpdateUserAsync(p);
        }
    }
}
