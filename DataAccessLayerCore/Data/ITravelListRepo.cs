using DataAccessLayerCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerCore.Data
{
    public interface ITravelListRepo
    {
        bool SaveChanges();
        IEnumerable<TravelList> GetAllTravelLists();
        TravelList GetTravelListById(int id);
        void CreateTravelList(TravelList tl);
        void UpdateTravelList(TravelList tl);
        void DeleteTravelList(TravelList tl);
    }
}
