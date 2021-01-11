using System.Collections.Generic;
using System.Threading.Tasks;
using TravelListModels;

namespace TravelListRepository
{
    public interface ITravelListItemRepo
    {
        bool SaveChanges();
        Task<IEnumerable<TravelListItem>> GetAllTravelLists(string userId);
        Task<TravelListItem> GetTravelListById(int id);
        Task<TravelListItem> CreateTravelList(TravelListItem tl);
        Task UpdateTravelList(int id, TravelListItem tl);
        Task DeleteTravelList( TravelListItem tl);
    }
}
