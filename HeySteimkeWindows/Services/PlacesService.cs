using HeySteimke.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HeySteimke.Services
{
    public class PlacesService
    {
        IDataBase<Item, Person, Place> dataStore;
        internal PlacesService(IDataBase<Item, Person, Place> dataStore)
        {
            this.dataStore = dataStore;
        }

        public async Task<IEnumerable<Place>> GetAllAsync()
        {
            var places = await dataStore.GetPlacesAsync();
            List<Place> retlist = new List<Place>();
            var profile = await dataStore.GetProfileAsync();
            foreach(var place in places)
            {
                if(place.Type == PlaceType._public || place.CreatorId == profile.Id)
                {
                    retlist.Add(place);
                }
                else
                {
                    // check if member of place
                    foreach(var memid in place.Members)
                    {
                        if(memid == profile.Id)
                        {
                            retlist.Add(place);
                        }
                    }
                }
            }
            return retlist;
        }

        public async Task CreateAsync(Place p)
        {
            var places = await dataStore.GetPlacesAsync();
            int id = 10;
            bool changed = true;
            while (changed)
            {
                changed = false;
                foreach(var pl in places)
                {
                    if(pl.Id == id)
                    {
                        changed = true;
                        id++;
                    }
                }
            }
            p.Id = id;
            var profile = await dataStore.GetProfileAsync();
            p.CreatorId = profile.Id;
            if(profile.Id <= 0 || profile.Name.Trim().Length <= 3)
            {
                return;
            }
            await dataStore.AddPlaceAsync(p);
        }

        public Task<Place> GetAsync(int place)
        {
            return dataStore.GetPlaceAsync(place);
        }

        public async Task<int> GetTodoItemCount(int place)
        {
            var items = await dataStore.QueryItemsAsync(place);
            int count = 0;
            foreach(var it in items)
            {
                if (it.ItemState == ItemState.created)
                    count++;
            }
            return count;
        }
    }
}
