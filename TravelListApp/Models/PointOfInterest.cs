using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelListModels;
using Windows.Devices.Geolocation;
using Windows.Foundation;

namespace TravelListApp.Models
{
    public class PointOfInterest : TravelPointOfInterest
    {
        public Guid LocalId { get; } = Guid.NewGuid();
        public Geopoint Location { get; set; }
        public Uri ImageSourceUri { get; set; }
        public Point NormalizedAnchorPoint { get; set; }
        public Boolean IsNew { get; set; }
        public Boolean IsUpdate { get; set; }
        public Boolean ToRemove { get; set; }
        public String PinIconName { get; } = "Pin";
    }
}
