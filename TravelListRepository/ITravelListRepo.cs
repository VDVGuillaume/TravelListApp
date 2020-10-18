using TravelListModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelListRepository
{
    public interface ITravelListRepo
    {
        bool SaveChanges();
        Task<IEnumerable<TravelList>> GetAllTravelLists();
        Task<TravelList> GetTravelListById(int id);
        Task CreateTravelList(TravelList tl);
        Task UpdateTravelList(TravelList tl);
        Task DeleteTravelList(TravelList tl);
    }
}
