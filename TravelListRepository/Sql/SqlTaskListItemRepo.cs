using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelListModels;

namespace TravelListRepository.Sql
{
    public class SqlTaskListItemRepo : ITaskListItemRepo
    {
        private readonly TravelListContext _context;

        public SqlTaskListItemRepo(TravelListContext context)
        {
            _context = context;
        }

        public async Task<TravelTaskListItem> CreateTaskListItemAsync(TravelTaskListItem taskListItem)
        {
            if (taskListItem == null)
            {
                throw new ArgumentNullException(nameof(taskListItem));
            }
            _context.Tasks.Add(taskListItem);
            await _context.SaveChangesAsync();
            return await _context.Tasks.AsNoTracking()
              .FirstOrDefaultAsync(p => p.TravelTaskListItemID == taskListItem.TravelTaskListItemID);
        }

        public async Task DeleteTaskListItemAsync(TravelTaskListItem taskListItem)
        {
            if (taskListItem == null)
            {
                throw new ArgumentNullException(nameof(taskListItem));
            }
            _context.Tasks.Remove(taskListItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskListItemAsync(int id, TravelTaskListItem taskListItem)
        {
            if (taskListItem == null)
            {
                throw new ArgumentNullException(nameof(taskListItem));
            }
            _context.Tasks.Update(taskListItem);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TravelTaskListItem>> GetTaskList(int travelListItemId)
        {
            return await _context.Tasks.AsNoTracking().Where(taskListItem => taskListItem.TravelListItemID == travelListItemId).ToListAsync();


        }


        public async Task<TravelTaskListItem> GetTaskListItemById(int id)
        {

            return await _context.Tasks.AsNoTracking().FirstOrDefaultAsync(p => p.TravelTaskListItemID == id);
        }


        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }


    }
}