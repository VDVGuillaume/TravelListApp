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

        public async Task CreateCheckListItem(CheckListItem checkListItem)
        {
            if (checkListItem == null)
            {
                throw new ArgumentNullException(nameof(checkListItem));
            }
            _context.Items.Add(checkListItem);
            await _context.SaveChangesAsync();
           
        }

        public async Task<IEnumerable<CheckListItem>> GetCheckList(int travelListItemId)
        {
            return await _context.Items.AsNoTracking().Where(checkListItem => checkListItem.TravelListItemID == travelListItemId).ToListAsync();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
