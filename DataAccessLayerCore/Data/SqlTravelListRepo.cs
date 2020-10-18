using DataAccessLayerCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerCore.Data
{
    public class SqlTravelListRepo : ITravelListRepo
    {
        private readonly TravelListContext _context;

        public SqlTravelListRepo(TravelListContext context)
        {
            _context = context;
        }

        public void CreateTravelList(TravelList tl)
        {
            if (tl == null)
            {
                throw new ArgumentNullException(nameof(tl));
            }
            _context.TravelLists.Add(tl);
        }

        public void DeleteTravelList(TravelList tl)
        {
            if (tl == null)
            {
                throw new ArgumentNullException(nameof(tl));
            }
            _context.TravelLists.Remove(tl);
        }

        public IEnumerable<TravelList> GetAllTravelLists()
        {
            return _context.TravelLists.ToList();
        }

        public TravelList GetTravelListById(int id)
        {
            return _context.TravelLists.FirstOrDefault(p => p.TravelListID == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateTravelList(TravelList tl)
        {
            //Nothing
        }
    }
}
