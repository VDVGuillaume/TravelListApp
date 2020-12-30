using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelListModels;

namespace TravelListRepository.Sql
{
    public class SqlTravelRouteRepo : ITravelRouteRepo
    {
        private readonly TravelListContext _context;

        public SqlTravelRouteRepo(TravelListContext context)
        {
            _context = context;
        }

        public async Task CreateTravelRoute(TravelRoute tl)
        {
            if (tl == null)
            {
                throw new ArgumentNullException(nameof(tl));
            }
            _context.Routes.Add(tl);
            await _context.SaveChangesAsync();
        }

        public async Task<TravelRoute> GetTravelRouteById(int id)
        {
            return await _context.Routes.AsNoTracking().FirstOrDefaultAsync(r => r.TravelRouteID == id);
        }

        public async Task DeleteTravelRoute(TravelRoute tl)
        {
            if (tl == null)
            {
                throw new ArgumentNullException(nameof(tl));
            }
            _context.Routes.Remove(tl);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTravelRoute(int id, TravelRoute tl)
        {
            if (tl == null)
            {
                throw new ArgumentNullException(nameof(tl));
            }
            _context.Routes.Update(tl);
            await _context.SaveChangesAsync();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
