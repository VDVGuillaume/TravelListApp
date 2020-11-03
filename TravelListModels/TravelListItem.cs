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
            this.Items = new List<CheckListItem>();
            this.Points = new List<TravelPointOfInterest>();
        }
        public int TravelListItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Country { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public virtual ICollection<CheckListItem> Items { get; set; }
        public virtual ICollection<TravelPointOfInterest> Points { get; set; }
    }
}
