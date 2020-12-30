using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelListModels
{
    public class TravelRoute
    {
        //public TravelRoute()
        //{
        //    ConnectedRoutes = new List<TravelRoutePointOfInterest>();
        //}
        [Key]
        public int TravelRouteID { get; set; }
        [Required]
        public int TravelListItemID { get; set; }
        // public virtual ICollection<TravelRoutePointOfInterest> ConnectedRoutes { get; set; }

        [ForeignKey("TravelPointOfInterest")]
        public int? StartTravelPointOfInterestID { get; set; }
        // [ForeignKey("StartTravelPointOfInterestID")]
        // public virtual TravelPointOfInterest Start { get; set; }
        [ForeignKey("TravelPointOfInterest")]
        public int? EndTravelPointOfInterestID { get; set; }
        // [ForeignKey("EndTravelPointOfInterestID")]
        // public virtual TravelPointOfInterest End { get; set; }
    }
}
