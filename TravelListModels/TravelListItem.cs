using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            this.Tasks = new List<TravelTaskListItem>();
            this.Routes = new List<TravelRoute>();
        }
        [Key]
        public int TravelListItemID { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public decimal Latitude { get; set; }
        [Required]
        public decimal Longitude { get; set; }
        public virtual ICollection<TravelCheckListItem> Items { get; set; }
        public virtual ICollection<TravelTaskListItem> Tasks { get; set; }
        public virtual ICollection<TravelPointOfInterest> Points { get; set; }
        public virtual ICollection<TravelListItemImage> Images { get; set; }
        public virtual ICollection<TravelRoute> Routes { get; set; }
    }
}
