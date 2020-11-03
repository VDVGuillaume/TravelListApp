using TravelListModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelListRepository
{
    public interface ITravelPointOfInterestRepo
    {
        bool SaveChanges();
        Task<TravelPointOfInterest> GetTravelPointOfInterestById(int id);
        Task CreateTravelPointOfInterest(TravelPointOfInterest tl);
        //Task UpdateTravelList(int id, TravelListItem tl);
        //Task DeleteTravelList( TravelListItem tl);
    }
}
