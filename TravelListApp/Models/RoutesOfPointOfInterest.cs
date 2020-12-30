using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelListModels;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.UI.Xaml.Controls.Maps;

namespace TravelListApp.Models
{
    public class RoutesOfPointOfInterest : TravelRoute
    {
        public RoutesOfPointOfInterest(TravelRoute route)
        {
            TravelRouteID = route.TravelRouteID;
            TravelListItemID = route.TravelListItemID;
            StartTravelPointOfInterestID = route.StartTravelPointOfInterestID;
            EndTravelPointOfInterestID = route.EndTravelPointOfInterestID;
        }
        public RoutesOfPointOfInterest(){}
        public Guid LocalId { get; } = Guid.NewGuid();
        public MapRouteView ViewOfRoute { get; set; }
        public TravelPointOfInterest Start { get; set; }
        public TravelPointOfInterest End { get; set; }
        public Boolean IsNew { get; set; }
        public Boolean IsUpdate { get; set; }
        public Boolean ToRemove { get; set; }
        public String PathIconName { get; } = "Path";
        public String ClearIconName { get; } = "Clear";
    }
}
