using TravelListModels;

namespace TravelListApp.Models
{
    public class TaskListItem : TravelTaskListItem
    {
        public bool IsNew { get; set; }
        public bool ToRemove { get; set; }
    }
}
