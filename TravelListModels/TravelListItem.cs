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
            this.Items = new List<TravelCheckListItem>();
            this.Points = new List<TravelPointOfInterest>();
            this.Images = new List<TravelListItemImage>();
        }

        public string UserId { get; set; }
        public int TravelListItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Country { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public virtual ICollection<TravelCheckListItem> Items { get; set; }
        public virtual ICollection<TravelPointOfInterest> Points { get; set; }
        public virtual ICollection<TravelListItemImage> Images { get; set; }
    }
}
