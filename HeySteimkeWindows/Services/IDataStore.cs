using HeySteimke.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeySteimke.Services
{
    public interface IDataStore
    {
        Task<ItemsService> GetItemsServiceAsync();
        Task<UserService> GetUserServiceAsync();
        Task<PlacesService> GetPlacesServiceAsync();
    }
}
