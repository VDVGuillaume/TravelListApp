using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelListModels;

namespace TravelListRepository
{
    public interface ITaskListItemRepo
    {
        bool SaveChanges();
        Task<IEnumerable<TravelTaskListItem>> GetTaskList(int travelListId);
        Task<TravelTaskListItem> CreateTaskListItemAsync(TravelTaskListItem taskListItem);
        Task<TravelTaskListItem> GetTaskListItemById(int id);
        Task DeleteTaskListItemAsync(TravelTaskListItem taskListItem);
        Task UpdateTaskListItemAsync(int id, TravelTaskListItem taskListItem);
    }
}
