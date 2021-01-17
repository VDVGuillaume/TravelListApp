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
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Latitude { get; set; }
        [Required]
        public decimal Longitude { get; set; }
        [Required]
        public int TravelListItemID { get; set; }
        [ForeignKey("StartTravelPointOfInterestID")]
        public virtual ICollection<TravelRoute> ConnectedStartRoutes { get; set; }
        [ForeignKey("EndTravelPointOfInterestID")]
        public virtual ICollection<TravelRoute> ConnectedEndRoutes { get; set; }
    }
}
