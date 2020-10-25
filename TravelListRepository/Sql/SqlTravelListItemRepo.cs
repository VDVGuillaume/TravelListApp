using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelListModels;

namespace TravelListRepository.Sql
{
    public class SqlTravelListItemRepo : ITravelListItemRepo
    {
        private readonly TravelListContext _context;

        public SqlTravelListItemRepo(TravelListContext context)
        {
            _context = context;
        }


        public async Task CreateTravelList(TravelListItem tl)
        {
            if (tl == null)
            {
                throw new ArgumentNullException(nameof(tl));
            }
            _context.TravelLists.Add(tl);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTravelList(TravelListItem tl)
        {
            if (tl == null)
            {
                throw new ArgumentNullException(nameof(tl));
            }
            _context.TravelLists.Remove(tl);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TravelListItem>> GetAllTravelLists()
        {
            return await _context.TravelLists.AsNoTracking().ToListAsync();
        }

        public async Task<TravelListItem> GetTravelListById(int id)
        {
            return await _context.TravelLists.AsNoTracking().FirstOrDefaultAsync(p => p.TravelListItemID == id);
        }

        public async Task UpdateTravelList(TravelListItem tl)
        {
            await _context.SaveChangesAsync();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
