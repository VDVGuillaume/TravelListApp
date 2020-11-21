using TravelListModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelListRepository
{
    public interface ITravelListItemRepo
    {
        bool SaveChanges();
        Task<IEnumerable<TravelListItem>> GetAllTravelLists();
        Task<TravelListItem> GetTravelListById(int id);
        Task<TravelListItem> CreateTravelList(TravelListItem tl);
        Task UpdateTravelList(int id, TravelListItem tl);
        Task DeleteTravelList( TravelListItem tl);
    }
}
