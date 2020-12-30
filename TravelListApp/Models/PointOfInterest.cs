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
        public PointOfInterest(TravelPointOfInterest point)
        {
            TravelPointOfInterestID = point.TravelPointOfInterestID;
            Name = point.Name;
            ImageSourceUri = new Uri("ms-appx:///Assets/MapPin.png");
            NormalizedAnchorPoint = new Point(0.5, 1);
            Latitude = (decimal)point.Latitude;
            Longitude = (decimal)point.Longitude;
            Location = new Geopoint(new BasicGeoposition()
            {
                Latitude = (double)point.Latitude,
                Longitude = (double)point.Longitude
            });
            TravelListItemID = point.TravelListItemID;
            ConnectedStartRoutes = point.ConnectedStartRoutes;
            ConnectedEndRoutes = point.ConnectedEndRoutes;
        }
        public PointOfInterest() { }
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
