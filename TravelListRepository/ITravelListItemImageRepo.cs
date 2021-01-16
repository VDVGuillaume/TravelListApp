using TravelListModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelListRepository
{
    public interface ITravelListItemImageRepo
    {
        bool SaveChanges();
        //Task<IEnumerable<TravelListItemImage>> GetAllTravelListImages();
        //Task<byte[]> GetTravelListImageDataById(int id);
        Task<TravelListItemImage> GetTravelListImageById(int id);
        Task CreateTravelListImage(TravelListItemImage tl);
        //Task UpdateTravelListImage(int id, TravelListItemImage tl);
        Task DeleteTravelListImage(TravelListItemImage tl);
    }
}
