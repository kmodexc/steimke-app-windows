using System;
using System.Collections.Generic;
using System.Text;

namespace HeySteimke.Models
{
    public enum PlaceType
    {
        _public,
        _private
    }
    public class Place
    {
        List<int> members;
        public Place()
        {
            members = new List<int>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public PlaceType Type { get; set; }
        public int CreatorId { get; set; }
        public IEnumerable<int> Members
        {
            get
            {
                return members;
            }
        }
        public void AddMember(int id)
        {
            members.Add(id);
        }
        public void RemoveMember(int id)
        {
            members.Remove(id);
        }
        public void ClearMembers()
        {
            members.Clear();
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
