using HeySteimke.Models;
using Microsoft.Rest.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HeySteimke.Services
{
    class JsonWrapper
    {
        JsonSerializerSettings settings;
        public JsonWrapper()
        {
            settings = new JsonSerializerSettings();
        }
        public Task<string> Serialize(Item it)
        {
            return Task.FromResult(SafeJsonConvert.SerializeObject(it, settings));
        }

        public Task<Item> DeserializeItem(string text)
        {
            return Task.FromResult(SafeJsonConvert.DeserializeObject<Item>(text, settings));
        }

        public Task<string> Serialize(Person it)
        {
            return Task.FromResult(SafeJsonConvert.SerializeObject(it, settings));
        }

        public Task<Person> DeserializePerson(string text)
        {
            return Task.FromResult(SafeJsonConvert.DeserializeObject<Person>(text, settings));
        }
    }
}
