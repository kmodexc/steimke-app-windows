using System;
using System.Collections.Generic;
using System.Text;

namespace HeySteimke.Models
{
    public enum MenuItemType
    {
        Places,
        ToDo,
        InProgress,
        Finished,
        OwnTasks,
        DataAnalysis,
        Settings
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }
        public string Title { get; set; }
    }
}
