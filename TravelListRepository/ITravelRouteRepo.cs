using TravelListModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelListRepository
{
    public interface ITravelRouteRepo
    {
        bool SaveChanges();
        Task<TravelRoute> GetTravelRouteById(int id);
        Task CreateTravelRoute(TravelRoute tl);
        Task UpdateTravelRoute(int id, TravelRoute tl);
        Task DeleteTravelRoute(TravelRoute tl);
    }
}
