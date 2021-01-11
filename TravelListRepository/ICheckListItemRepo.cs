using System.Collections.Generic;
using System.Threading.Tasks;
using TravelListModels;

namespace TravelListRepository
{
    public interface ICheckListItemRepo
    {
        bool SaveChanges();
        Task<IEnumerable<TravelCheckListItem>> GetCheckList(int travelListId);
        Task CreateCheckListItemAsync(TravelCheckListItem checkListItem);
        Task<TravelCheckListItem> GetCheckListItemById(int id);
        Task DeleteCheckListItemAsync(TravelCheckListItem checkListItem);
        Task UpdateCheckListItemAsync(int id, TravelCheckListItem checkListItem);
    }
}
