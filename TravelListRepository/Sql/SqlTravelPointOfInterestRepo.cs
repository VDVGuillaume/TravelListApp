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

        public async Task<TravelPointOfInterest> GetTravelPointOfInterestById(int id)
        {
            return await _context.Points.AsNoTracking().FirstOrDefaultAsync(p => p.TravelPointOfInterestID == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
