using System.Collections.Generic;
using System.Threading.Tasks;
using TravelListModels;

namespace TravelListRepository
{
    public interface ICategoryRepo
    {
        bool SaveChanges();
        Task<IEnumerable<Category>> GetAllCategories(string userId);
        Task CreateCategory(Category category);
        
    }
}
