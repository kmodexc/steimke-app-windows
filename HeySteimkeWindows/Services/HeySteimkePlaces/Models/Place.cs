﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace HeySteimke.Services.Rest.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class Place
    {
        /// <summary>
        /// Initializes a new instance of the Place class.
        /// </summary>
        public Place() { }

        /// <summary>
        /// Initializes a new instance of the Place class.
        /// </summary>
        public Place(int id, string type = default(string), string name = default(string), int? creatorId = default(int?), IList<int?> members = default(IList<int?>))
        {
            Id = id;
            Type = type;
            Name = name;
            CreatorId = creatorId;
            Members = members;
        }

        /// <summary>
        /// Identifier
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// State. Possible values include: 'public', 'private'
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Name of Place
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// id of creator
        /// </summary>
        [JsonProperty(PropertyName = "creatorId")]
        public int? CreatorId { get; set; }

        /// <summary>
        /// ids of members
        /// </summary>
        [JsonProperty(PropertyName = "members")]
        public IList<int?> Members { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
        }
    }
}