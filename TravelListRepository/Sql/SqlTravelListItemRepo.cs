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


        public async Task<TravelListItem> CreateTravelList(TravelListItem tl)
        {
            if (tl == null)
            {
                throw new ArgumentNullException(nameof(tl));
            }
            _context.TravelLists.Add(tl);
            await _context.SaveChangesAsync();
            return await _context.TravelLists.AsNoTracking().Include(x => x.Points).Include(x => x.Items).Include(x => x.Images).FirstOrDefaultAsync(p => p.TravelListItemID == tl.TravelListItemID);
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

        public async Task<IEnumerable<TravelListItem>> GetAllTravelLists(string userId)
        {
            return await _context.TravelLists.AsNoTracking().Where(x=> x.UserId==userId).Include(x => x.Points).Include(x => x.Items).Include(x => x.Images).ToListAsync();
        }

        public async Task<TravelListItem> GetTravelListById(int id)
        {
            return await _context.TravelLists.AsNoTracking().Include(x => x.Points).Include(x => x.Items).Include(x => x.Images).FirstOrDefaultAsync(p => p.TravelListItemID == id);
        }

        public async Task UpdateTravelList(int id, TravelListItem tl)
        {
            if (tl == null)
            {
                throw new ArgumentNullException(nameof(tl));
            }
            _context.TravelLists.Update(tl);
            //TravelListItem travelList = await _context.TravelLists.FirstAsync(p => p.TravelListItemID == id);
            //travelList = tl;
            await _context.SaveChangesAsync();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
