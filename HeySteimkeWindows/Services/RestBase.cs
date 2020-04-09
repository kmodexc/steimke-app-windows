using System;
using System.Collections.Generic;
using System.Text;

namespace HeySteimke.Services
{
    class RestBase
    {
        public HeySteimke.Rest.HeySteimkeBase.Models.Item toRestItem(Models.Item it)
        {
            var restitem = new HeySteimke.Rest.HeySteimkeBase.Models.Item();
            restitem.AssignedId = it.AssignedId;
            restitem.Assignedtime = it.Assignedtime;
            restitem.Createtime = it.Createtime;
            restitem.CreatorId = it.CreatorId;
            restitem.Desc = it.Desc;
            restitem.Finishedtime = it.Finishedtime;
            restitem.Id = it.Id;
            restitem.Name = it.Name;
            restitem.Placeid = it.Placeid;
            restitem.Priority = it.Priority;
            restitem.Shortdesc = it.Shortdesc;
            restitem.State = it.State;
            restitem.Workload = it.Workload;
            return restitem;
        }
        public Models.Item toAppItem(HeySteimke.Rest.HeySteimkeBase.Models.Item it)
        {
            var restitem = new Models.Item();
            restitem.AssignedId = it.AssignedId;
            restitem.Assignedtime = it.Assignedtime;
            restitem.Createtime = it.Createtime;
            restitem.CreatorId = it.CreatorId;
            restitem.Desc = it.Desc;
            restitem.Finishedtime = it.Finishedtime;
            restitem.Id = it.Id;
            restitem.Name = it.Name;
            restitem.Placeid = it.Placeid;
            restitem.Priority = it.Priority;
            restitem.Shortdesc = it.Shortdesc;
            restitem.State = it.State;
            restitem.Workload = it.Workload;
            return restitem;
        }

        public HeySteimke.Services.Rest.HeySteimkeUser.Models.User toRestUser(Models.Person user)
        {
            var restuser = new HeySteimke.Services.Rest.HeySteimkeUser.Models.User();
            restuser.Id = user.Id;
            restuser.Name = user.Name;
            restuser.State = user.State.ToString();
            restuser.Workload = user.Workload;
            return restuser;
        }

        public Models.Person toAppUser(HeySteimke.Services.Rest.HeySteimkeUser.Models.User restuser)
        {
            var user = new Models.Person();
            user.Id = restuser.Id;
            user.Name = restuser.Name;
            user.Place = 0;
            user.State = (Models.UserState)Enum.Parse(typeof(Models.UserState), restuser.State);
            user.Workload = restuser.Workload.GetValueOrDefault();
            return user;
        }

        public HeySteimke.Rest.HeySteimkeBase.Models.TimeStamp createTimeStamp()
        {
            return new HeySteimke.Rest.HeySteimkeBase.Models.TimeStamp();
        }

        public HeySteimke.Services.Rest.Models.Place toRestPlace(Models.Place place)
        {
            var restplace = new HeySteimke.Services.Rest.Models.Place();
            restplace.Id = place.Id;
            restplace.Name = place.Name;
            restplace.Type = place.Type.ToString();
            restplace.Type = restplace.Type.Replace('_', ' ').Trim();
            restplace.CreatorId = place.CreatorId;
            restplace.Members = new List<int?>();
            foreach (var mem in place.Members)
            {
                restplace.Members.Add(mem);
            }
            return restplace;
        }

        public Models.Place toAppPlace(HeySteimke.Services.Rest.Models.Place restplace)
        {
            var user = new Models.Place();
            user.Id = restplace.Id;
            user.Name = restplace.Name;
            user.Type = (Models.PlaceType)Enum.Parse(typeof(Models.PlaceType), "_"+restplace.Type);
            user.CreatorId = restplace.CreatorId.GetValueOrDefault();
            foreach(var mem in restplace.Members)
            {
                if(mem.HasValue)
                    user.AddMember(mem.Value);
            }
            return user;
        }
    }
}
