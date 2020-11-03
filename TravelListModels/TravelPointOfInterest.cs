using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelListModels
{
    public class TravelPointOfInterest
    {
        public int TravelPointOfInterestID { get; set; }
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int TravelListItemID { get; set; }
        // public virtual TravelListItem TravelListItem { get; set; }
    }
}
