using HeySteimke.Models;
using HeySteimke.Rest.HeySteimkeBase;
using HeySteimke.Services.Rest;
using HeySteimke.Services.Rest.HeySteimkeUser;
using Microsoft.Rest;
using Microsoft.Rest.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HeySteimke.Services
{
    class NetworkDataStore
    {
        IHeySteimkeBaseClient api_item;
        IHeySteimkeUserClient api_user;
        IHeySteimkePlaces api_places;
        RestBase restBase;

        public NetworkDataStore()
        {
            api_item = new HeySteimkeBaseClient(null);
            api_user = new HeySteimkeUserClient(null);
            api_places = new HeySteimkePlaces(null);
            restBase = new RestBase();
        }
        public async Task<bool> AddItemAsync(Item item)
        {
            try
            {
                await api_item.AddItemAsync(restBase.toRestItem(item));
                return true;
            }
            catch (Exception exc)
            {
                Debug.WriteLine("Error while add item");
                Debug.WriteLine(exc);
            }
            return false;
        }

        public async Task AddPlaceAsync(Place p)
        {
            try
            {
                await api_places.AddPlaceAsync(restBase.toRestPlace(p));
            }
            catch (Exception exc)
            {
                Debug.WriteLine("Error while add place");
                Debug.WriteLine(exc);
            }
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            try
            {
                await api_item.DeleteItemAsync(id);
                return true;
            }
            catch (Exception exc)
            {
                Debug.WriteLine("Error while deleting");
                Debug.WriteLine(exc);
            }
            return false;
        }

        public async Task<Place> GetPlaceAsync(int id)
        {
            try
            {
                var it = await api_places.GetPlaceAsync(id);
                return restBase.toAppPlace(it);
            }
            catch (Exception exc)
            {
                Debug.WriteLine("Error while get item");
                Debug.WriteLine(exc);
            }
            return new Place();
        }

        public async Task<Item> GetItemAsync(int id)
        {
            try
            {
                var it = await api_item.GetItemAsync(id);
                return restBase.toAppItem(it);
            }
            catch (Exception exc)
            {
                Debug.WriteLine("Error while get item");
                Debug.WriteLine(exc);
            }
            return new Item();
        }

        public async Task<IList<Place>> GetPlacesAsync()
        {
            try
            {
                IList<int?> idlist = await api_places.GetAvaiPlaceIdsAsync();
                IList<Place> items = new List<Place>();
                foreach (int id in idlist)
                {
                    items.Add(await GetPlaceAsync(id));
                }
                return items;
            }
            catch (Exception exc)
            {
                Debug.WriteLine("Error while get item list");
                Debug.WriteLine(exc);
            }
            return new List<Place>();
        }

        public async Task AddUserAsync(Person p)
        {
            try
            {
                await api_user.AddUserAsync(restBase.toRestUser(p));
            }
            catch (Exception exc)
            {
                Debug.WriteLine("Error while add item");
                Debug.WriteLine(exc);
            }
        }

        public async Task<IList<Item>> GetItemsAsync()
        {
            try
            {
                IList<int?> idlist = await api_item.GetAvaiIdsAsync();
                IList<Item> items = new List<Item>();
                foreach (int id in idlist)
                {
                    items.Add(await GetItemAsync(id));
                }
                return items;
            }
            catch (Exception exc)
            {
                Debug.WriteLine("Error while get item list");
                Debug.WriteLine(exc);
            }
            return new List<Item>();
        }

        public async Task<Person> GetUserAsync(int id)
        {
            try
            {

                var restuser = await api_user.GetUserAsync(id);
                return restBase.toAppUser(restuser);
            }
            catch(Exception exc)
            {
                Debug.WriteLine("Error while get user");
                Debug.WriteLine(exc);
            }
            return new Person();
        }
        public async Task<bool> UpdateItemAsync(Item item)
        {
            try
            {
                var it = restBase.toRestItem(item);
                await api_item.ReplaceItemAsync(item.Id, it);
                return true;
            }
            catch (Exception exc)
            {
                Debug.WriteLine("Error while update item");
                Debug.WriteLine(exc);
            }
            return false;
        }
        public async Task<IList<Person>> GetUsersAsync(bool forceRefresh = false)
        {
            var ids = api_user.GetAvaiUserIds();
            var users = new List<Person>();
            foreach (int id in ids)
            {
                if (id > 0)
                    users.Add(await GetUserAsync(id));
            }
            return users;
        }

        public async Task UpdateUserAsync(Person p)
        {
            try
            {
                await api_user.ReplaceUserAsync(p.Id, restBase.toRestUser(p));
            }catch(Exception exc)
            {
                Debug.WriteLine(exc);
            }
        }

        public async Task<IList<Item>> QueryItemsAsync(int placeId)
        {
            var items = await GetItemsAsync();
            var retlist = new List<Item>();
            foreach (var it in items)
            {
                if (it.Placeid == placeId || it.Placeid == null)
                    retlist.Add(it);
            }
            return retlist;
        }
    }
}
