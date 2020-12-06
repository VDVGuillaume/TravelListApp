using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelListModels;

namespace TravelListRepository.Sql
{
    public class SqlTravelListItemImageRepo : ITravelListItemImageRepo
    {
        private readonly TravelListContext _context;

        public SqlTravelListItemImageRepo(TravelListContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TravelListItemImage>> GetAllTravelListImages()
        {
            return await _context.TravelListImages.AsNoTracking().ToListAsync();
        }

        public async Task<byte[]> GetTravelListImageDataById(int id)
        {
            TravelListItemImage tl = await _context.TravelListImages.AsNoTracking().FirstOrDefaultAsync(p => p.TravelListItemImageID == id);
            return tl.ImageData;
        }

        public async Task<TravelListItemImage> GetTravelListImageById(int id)
        {
            
            return await _context.TravelListImages.AsNoTracking().FirstOrDefaultAsync(p => p.TravelListItemImageID == id);
        }


        public async Task CreateTravelListImage(TravelListItemImage tl)
        {
            if (tl == null)
            {
                throw new ArgumentNullException(nameof(tl));
            }
            _context.TravelListImages.Add(tl);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTravelListImage(TravelListItemImage tl)
        {
            if (tl == null)
            {
                throw new ArgumentNullException(nameof(tl));
            }
            _context.TravelListImages.Remove(tl);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTravelListImage(int id, TravelListItemImage tl)
        {
            if (tl == null)
            {
                throw new ArgumentNullException(nameof(tl));
            }
            _context.TravelListImages.Update(tl);
            await _context.SaveChangesAsync();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
