using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeySteimke.Models
{
    public enum UserState
    {
        admin,
        user,
        guest
    }
    public enum Places
    {
        Haupthaus,
        Seminarhaus,
        Jugendherberge,
        Pferdestall,
        Huenerstall
    }
    public class Person
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "wl")]
        public int Workload { get; set; }
        [JsonProperty(PropertyName = "state")]
        public UserState State { get; set; }
        [JsonProperty(PropertyName = "place")]
        public int Place { get; set; }
        public override string ToString()
        {
            return Name;
        }

    }
}
