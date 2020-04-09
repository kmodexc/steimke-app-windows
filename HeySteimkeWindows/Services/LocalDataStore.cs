using HeySteimke.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HeySteimke.Services
{
    class LocalDataStore
    {
        string profile_fileName;
        FileWrapper fileWrapper;
        JsonWrapper jsonWrapper;
        public LocalDataStore()
        {
            fileWrapper = new FileWrapper();
            profile_fileName = fileWrapper.GetLocalAppDataFile("profile.json");
            jsonWrapper = new JsonWrapper();
        }
        public async Task<Person> GetProfileAsync()
        {
            if (!(await fileWrapper.ExistsAsync(profile_fileName)))
                return new Person();
            var content = await fileWrapper.ReadAll(profile_fileName);
            return await jsonWrapper.DeserializePerson(content);
        }

        public async Task SetProfileAsync(Person p)
        {
            var content = await jsonWrapper.Serialize(p);
            await fileWrapper.WriteAll(profile_fileName, content);
        }

        public Task<bool> ExistsProfileAsync()
        {
            return fileWrapper.ExistsAsync(profile_fileName);
        }
    }
}
