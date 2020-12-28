using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelListModels;

namespace TravelListRepository.Sql
{
    public class SqlTravelPointOfInterestRepo : ITravelPointOfInterestRepo
    {
        private readonly TravelListContext _context;

        public SqlTravelPointOfInterestRepo(TravelListContext context)
        {
            _context = context;
        }

        public async Task CreateTravelPointOfInterest(TravelPointOfInterest tl)
        {
            if (tl == null)
            {
                throw new ArgumentNullException(nameof(tl));
            }
            _context.Points.Add(tl);
            await _context.SaveChangesAsync();
        }

        public async Task<TravelPointOfInterest> GetTravelPointOfInterestById(int id)
        {
            return await _context.Points.AsNoTracking().FirstOrDefaultAsync(p => p.TravelPointOfInterestID == id);
        }

        public async Task DeleteTravelPointOfInterest(TravelPointOfInterest tl)
        {
            if (tl == null)
            {
                throw new ArgumentNullException(nameof(tl));
            }
            _context.Points.Remove(tl);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTravelPointOfInterest(int id, TravelPointOfInterest tl)
        {
            if (tl == null)
            {
                throw new ArgumentNullException(nameof(tl));
            }
            _context.Points.Update(tl);
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
