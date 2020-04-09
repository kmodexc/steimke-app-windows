﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace HeySteimke.Rest.HeySteimkeBase.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class TimeStamp
    {
        /// <summary>
        /// Initializes a new instance of the TimeStamp class.
        /// </summary>
        public TimeStamp() { }

        /// <summary>
        /// Initializes a new instance of the TimeStamp class.
        /// </summary>
        public TimeStamp(int? year = default(int?), int? month = default(int?), int? day = default(int?), int? hour = default(int?), int? minute = default(int?))
        {
            Year = year;
            Month = month;
            Day = day;
            Hour = hour;
            Minute = minute;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "year")]
        public int? Year { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "month")]
        public int? Month { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "day")]
        public int? Day { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "hour")]
        public int? Hour { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "minute")]
        public int? Minute { get; set; }


    }
}
