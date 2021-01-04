using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelListModels
{
    public class TravelRoute
    {
        [Key]
        public int TravelRouteID { get; set; }
        [Required]
        public int TravelListItemID { get; set; }
        [Required]
        public bool Driving { get; set; }
        [ForeignKey("TravelPointOfInterest")]
        public int? StartTravelPointOfInterestID { get; set; }
        [ForeignKey("TravelPointOfInterest")]
        public int? EndTravelPointOfInterestID { get; set; }
    }
}
