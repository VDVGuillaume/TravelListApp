using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelListModels
{
    public class TravelPointOfInterest
    {
        public TravelPointOfInterest()
        {
            ConnectedStartRoutes = new List<TravelRoute>();
            ConnectedEndRoutes = new List<TravelRoute>();
        }
        [Key]
        public int TravelPointOfInterestID { get; set; }
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int TravelListItemID { get; set; }
        [ForeignKey("StartTravelPointOfInterestID")]
        // [InverseProperty("Start")]
        public virtual ICollection<TravelRoute> ConnectedStartRoutes { get; set; }
        [ForeignKey("EndTravelPointOfInterestID")]
        // [InverseProperty("Start")]
        public virtual ICollection<TravelRoute> ConnectedEndRoutes { get; set; }
    }
}
