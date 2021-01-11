using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelListModels;

namespace TravelListRepository.Sql
{
    public class SqlCheckListItemRepo : ICheckListItemRepo
    {
        private readonly TravelListContext _context;

        public SqlCheckListItemRepo(TravelListContext context)
        {
            _context = context;
        }

        public async Task CreateCheckListItemAsync(TravelCheckListItem checkListItem)
        {
            if (checkListItem == null)
            {
                throw new ArgumentNullException(nameof(checkListItem));
            }
            _context.Items.Add(checkListItem);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteCheckListItemAsync(TravelCheckListItem checkListItem)
        {
            if (checkListItem == null)
            {
                throw new ArgumentNullException(nameof(checkListItem));
            }
            _context.Items.Remove(checkListItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCheckListItemAsync(int id, TravelCheckListItem checkListItem)
        {
            if (checkListItem == null)
            {
                throw new ArgumentNullException(nameof(checkListItem));
            }
            _context.Items.Update(checkListItem);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TravelCheckListItem>> GetCheckList(int travelListItemId)
        {
            return await _context.Items.AsNoTracking().Where(checkListItem => checkListItem.TravelListItemID == travelListItemId).Include(x => x.Category).ToListAsync();


        }


        public async Task<TravelCheckListItem> GetCheckListItemById(int id)
        {

            return await _context.Items.AsNoTracking().FirstOrDefaultAsync(p => p.TravelCheckListItemID == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }


    }
}