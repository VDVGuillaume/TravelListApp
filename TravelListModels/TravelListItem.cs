using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelListModels
{
    public class TravelListItem
    {
        public TravelListItem()
        {
            Items = new HashSet<CheckListItem>();
        }
        public int TravelListItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Country { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public virtual ICollection<CheckListItem> Items { get; set; }
    }
}
