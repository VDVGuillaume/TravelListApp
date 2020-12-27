using System.Collections.Generic;
using System.Threading.Tasks;
using TravelListModels;

namespace TravelListRepository
{
    public interface ICheckListItemRepo
    {
        bool SaveChanges();
        Task<IEnumerable<CheckListItem>> GetCheckList(int travelListId);        
        Task CreateCheckListItem(CheckListItem checkListItem);
       
    }
}
