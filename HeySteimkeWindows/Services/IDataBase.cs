using HeySteimke.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HeySteimke.Services
{
    interface IDataBase<T,U,P>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(int id);
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        Task<IEnumerable<T>> QueryItemsAsync(int placeId);
        Task<U> GetUserAsync(int id);
        Task<IEnumerable<U>> GetUsersAsync(bool forceRefresh = false);
        Task<U> GetProfileAsync();
        Task SetProfileAsync(U p);
        Task UpdateUserAsync(U p);
        Task<IEnumerable<P>> GetPlacesAsync(bool forceRefresh = false);
        Task<P> GetPlaceAsync(int id);
        Task AddPlaceAsync(Place p);
    }
}
