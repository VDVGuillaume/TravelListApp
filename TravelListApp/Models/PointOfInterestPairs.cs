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
    public class PointOfInterestPairs 
    {
        public PointOfInterest Start { get; set; }
        public PointOfInterest End { get; set; }
    }
}
