using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelListModels;

namespace TravelListRepository.Sql
{
    public class SqlCategoryRepo : ICategoryRepo
    {
        private readonly TravelListContext _context;

        public SqlCategoryRepo(TravelListContext context)
        {
            _context = context;
        }


        public async Task CreateCategory(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategories(string userId)
        {
            return await _context.Categories.AsNoTracking().Where(x => x.UserId == userId).ToListAsync();
        }
              
        

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
