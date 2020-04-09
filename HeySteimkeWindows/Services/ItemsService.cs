using HeySteimke.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HeySteimke.Services
{
    public class ItemsService
    {
        IDataBase<Item, Person, Place> dataStore;
        internal ItemsService(IDataBase<Item, Person, Place> dataStore)
        {
            this.dataStore = dataStore;
        }
        public async Task<int> Create(Item item = null)
        {
            int id = 10;
            bool changed = true;
            while (changed)
            {
                changed = false;
                foreach (Item it in await dataStore.GetItemsAsync())
                {
                    if (it.Id == id)
                    {
                        id++;
                        changed = true;
                    }
                }
            }
            if (item == null)
                item = new Item(id);
            else item.Id = id;

            var profile = await dataStore.GetProfileAsync();

            item.Placeid = (int)profile.Place;
            item.CreatorId = profile.Id;

            if(profile.Name.Trim().Length <= 3 || profile.Id <= 0)
            {
                return -1;
            }

            if (item.Workload == null)
                item.Workload = 0;
            if (item.Name == null || item.Name.Length <= 1)
                item.Name = "[ohne Titel]";

            var time = new HeySteimke.Rest.HeySteimkeBase.Models.TimeStamp();
            time.Year = DateTime.Now.Year;
            time.Month = DateTime.Now.Month;
            time.Day = DateTime.Now.Day;
            time.Hour = DateTime.Now.Hour;
            time.Minute = DateTime.Now.Minute;
            item.Createtime = time;

            await dataStore.AddItemAsync(item);
            return id;
        }

        public async Task<IEnumerable<Item>> GetFinishedByAsync(int id)
        {
            var items = await dataStore.GetItemsAsync();
            var retlist = new List<Item>();
            foreach (var item in items)
            {
                if (item.ItemState == ItemState.finished && item.AssignedId == id)
                {
                    retlist.Add(item);
                }
            }
            return retlist;
        }

        public async Task<IEnumerable<Item>> GetAssignedToAsync(int id)
        {
            var items = await dataStore.GetItemsAsync();
            var retlist = new List<Item>();
            foreach(var item in items)
            {
                if(item.ItemState == ItemState.assigned && item.AssignedId == id)
                {
                    retlist.Add(item);
                }
            }
            return retlist;
        }

        public async Task<Item> Get(int id)
        {
            return await dataStore.GetItemAsync(id);
        }

        public async Task Update(Item it)
        {
            await dataStore.UpdateItemAsync(it);
        }

        public async Task Assigne(Item it)
        {
            var prof = await dataStore.GetProfileAsync();
            int ownid = prof.Id;
            it.AssignedId = ownid;
            it.ItemState = ItemState.assigned;

            var time = new HeySteimke.Rest.HeySteimkeBase.Models.TimeStamp();
            time.Year = DateTime.Now.Year;
            time.Month = DateTime.Now.Month;
            time.Day = DateTime.Now.Day;
            time.Hour = DateTime.Now.Hour;
            time.Minute = DateTime.Now.Minute;
            it.Assignedtime = time;

            await dataStore.UpdateItemAsync(it);
        }

        public async Task DelegateAsync(int itemid, int personid)
        {
            var item = await dataStore.GetItemAsync(itemid);

            item.ItemState = ItemState.assigned;
            item.AssignedId = personid;

            var time = new HeySteimke.Rest.HeySteimkeBase.Models.TimeStamp();
            time.Year = DateTime.Now.Year;
            time.Month = DateTime.Now.Month;
            time.Day = DateTime.Now.Day;
            time.Hour = DateTime.Now.Hour;
            time.Minute = DateTime.Now.Minute;
            item.Assignedtime = time;

            await dataStore.UpdateItemAsync(item);
        }

        public async Task Finish(Item it)
        {
            // update item
            it.ItemState = ItemState.finished;

            var time = new HeySteimke.Rest.HeySteimkeBase.Models.TimeStamp();
            time.Year = DateTime.Now.Year;
            time.Month = DateTime.Now.Month;
            time.Day = DateTime.Now.Day;
            time.Hour = DateTime.Now.Hour;
            time.Minute = DateTime.Now.Minute;
            it.Finishedtime = time;

            await dataStore.UpdateItemAsync(it);


            // add workload to user
            var profile = await dataStore.GetProfileAsync();
            var user = await dataStore.GetUserAsync(profile.Id);
            user.Workload += it.Workload.GetValueOrDefault();
            await dataStore.UpdateUserAsync(user);
        }

        public async Task RefreshAsync()
        {
            await dataStore.GetItemsAsync(true);
        }

        public async Task Delete(Item it)
        {
            await dataStore.DeleteItemAsync(it.Id);
        }

        public async Task<IEnumerable<Item>> GetOpen()
        {
            List<Item> retitem = new List<Item>();
            var profile = await dataStore.GetProfileAsync();
            var inlist = await dataStore.QueryItemsAsync((int)profile.Place);
            foreach (var it in inlist)
            {
                if (it.ItemState == ItemState.created)
                    retitem.Add(it);
            }
            return retitem;
        }

        public async Task<IEnumerable<Item>> GetInProgress()
        {
            List<Item> retitem = new List<Item>();
            var profile = await dataStore.GetProfileAsync();
            var inlist = await dataStore.QueryItemsAsync((int)profile.Place);
            foreach (var it in inlist)
            {
                if (it.ItemState == ItemState.inprogress || it.ItemState == ItemState.assigned)
                    retitem.Add(it);
            }
            return retitem;
        }

        public async Task<IEnumerable<Item>> GetFinished()
        {
            List<Item> retitem = new List<Item>();
            var profile = await dataStore.GetProfileAsync();
            var inlist = await dataStore.QueryItemsAsync((int)profile.Place);
            foreach (var it in inlist)
            {
                if (it.ItemState == ItemState.finished || it.ItemState == ItemState.paused)
                    retitem.Add(it);
            }
            return retitem;
        }

        public async Task<IEnumerable<Item>> GetOwn()
        {
            List<Item> retitem = new List<Item>();
            var inlist = await dataStore.GetItemsAsync();
            var profile = await dataStore.GetProfileAsync();
            var ownid = profile.Id;
            foreach (var it in inlist)
            {
                if (it.AssignedId == ownid && (it.ItemState == ItemState.assigned || it.ItemState == ItemState.inprogress))
                    retitem.Add(it);
            }
            return retitem;
        }


    }
}
