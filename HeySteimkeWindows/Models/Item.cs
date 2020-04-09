using HeySteimke.Rest.HeySteimkeBase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeySteimke.Models
{
    public enum ItemState
    {
        created,
        assigned,
        inprogress,
        finished,
        paused,
        aborted
    }
    public class Item : HeySteimke.Rest.HeySteimkeBase.Models.Item
    {
        /// <summary>
        /// Initializes a new instance of the Item class.
        /// </summary>
        public Item() : this(-1) { }
                
        public Item(int id, ItemState state = default(ItemState), string name = default(string), string shortdesc = default(string), 
            string desc = default(string), int? creatorId = default(int?), int? assignedId = default(int?), 
            int? priority = default(int?), int? workload = default(int?), int? placeid = null,
            HeySteimke.Rest.HeySteimkeBase.Models.TimeStamp createTime = default(HeySteimke.Rest.HeySteimkeBase.Models.TimeStamp),
            HeySteimke.Rest.HeySteimkeBase.Models.TimeStamp assignedTime = default(HeySteimke.Rest.HeySteimkeBase.Models.TimeStamp),
            HeySteimke.Rest.HeySteimkeBase.Models.TimeStamp finishedTime = default(HeySteimke.Rest.HeySteimkeBase.Models.TimeStamp))
        {
            Id = id;
            ItemState = state;
            Name = name;
            Shortdesc = shortdesc;
            Desc = desc;
            CreatorId = creatorId;
            AssignedId = assignedId;
            Priority = priority;
            Workload = workload;
            Placeid = placeid;
            Createtime = createTime;
            Assignedtime = assignedTime;
            Finishedtime = finishedTime;
        }

        public Item(HeySteimke.Rest.HeySteimkeBase.Models.Item it) : base(it)
        {
        }

        public ItemState ItemState {
            get
            {
                return (ItemState)Enum.Parse(typeof(ItemState), State);
            }
            set
            {
                State = value.ToString();
            }
        }
    }
}
