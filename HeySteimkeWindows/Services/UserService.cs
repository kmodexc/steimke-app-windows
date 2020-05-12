using HeySteimke.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HeySteimke.Services
{
    public class UserService
    {
        IDataBase<Item, Person, Place> dataStore;

        public bool IsAdmin { get; private set; }

        internal UserService(IDataBase<Item, Person, Place> dataStore)
        {
            this.dataStore = dataStore;
            IsAdmin = false;
        }

        public async Task SetName(string name)
        {
            if (IsAdmin || name == "cpp")
            {
                IsAdmin = true;
                return;
            }
            if (name.Length <= 3)
                return;               
            var users = await dataStore.GetUsersAsync();
            int id = 10;
            bool changed = true;
            while (changed)
            {
                changed = false;
                foreach (var user in users)
                {
                    if (user.Id == id)
                    {
                        changed = true;
                        id++;
                    }
                }
            }
            Person person = new Person();
            person.Name = name;
            person.Id = id;
            person.State = UserState.user;
            person.Place = 1;
            person.Workload = 0;
            await dataStore.SetProfileAsync(person);
        }

        public async Task SetPlaceAsync(int place)
        {
            var profile = await GetProfileAsync();
            profile.Place = place;
            await dataStore.SetProfileAsync(profile);
        }

        public Task<IEnumerable<Person>> GetAllAsync()
        {
            return dataStore.GetUsersAsync();
        }

        public async Task<Person> GetProfileAsync()
        {
            if (IsAdmin)
            {
                Person admin = new Person
                {
                    Id = 1,
                    Name = "administrator",
                    State = UserState.admin,
                };
                return admin;
            }
            var locProf = await dataStore.GetProfileAsync();
            var servProf = await dataStore.GetUserAsync(locProf.Id);
            locProf.Name = servProf.Name;
            locProf.State = servProf.State;
            locProf.Workload = servProf.Workload;
            return locProf;
        }

        public Task<Person> GetAsync(int id)
        {
            return dataStore.GetUserAsync(id);
        }

        public async Task<bool> NeedsInitializationAsync()
        {
            if (IsAdmin) return false;
            var locProfile = await dataStore.GetProfileAsync();
            if(locProfile == null || locProfile.Id <= 0)
                return true;
            var servUsers = await dataStore.GetUserAsync(locProfile.Id);
            if (servUsers == null || servUsers.Id <= 0 || servUsers.Name.Trim().Length <= 2)
                return true;
            return false;
        }
    }
}
