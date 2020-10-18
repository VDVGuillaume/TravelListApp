using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelListModels
{
    public class TravelList
    {
        public TravelList()
        {
            Items = new HashSet<Item>();
        }
        public int TravelListID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
