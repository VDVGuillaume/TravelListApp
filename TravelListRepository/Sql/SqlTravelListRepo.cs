using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelListModels;

namespace TravelListRepository.Sql
{
    public class SqlTravelListRepo : ITravelListRepo
    {
        private readonly TravelListContext _context;

        public SqlTravelListRepo(TravelListContext context)
        {
            _context = context;
        }


        public async Task CreateTravelList(TravelList tl)
        {
            if (tl == null)
            {
                throw new ArgumentNullException(nameof(tl));
            }
            _context.TravelLists.Add(tl);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTravelList(TravelList tl)
        {
            if (tl == null)
            {
                throw new ArgumentNullException(nameof(tl));
            }
            _context.TravelLists.Remove(tl);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TravelList>> GetAllTravelLists()
        {
            return await _context.TravelLists.AsNoTracking().ToListAsync();
        }

        public async Task<TravelList> GetTravelListById(int id)
        {
            return await _context.TravelLists.AsNoTracking().FirstOrDefaultAsync(p => p.TravelListID == id);
        }

        public async Task UpdateTravelList(TravelList tl)
        {
            await _context.SaveChangesAsync();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
