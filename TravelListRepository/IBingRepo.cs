using System.Threading.Tasks;
using TravelListModels;

namespace TravelListRepository
{
    public interface IBingRepo
    {
        Task<Location> GetLocationByQuery(string search);
    }
}
