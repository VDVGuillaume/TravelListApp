using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace TravelListModels
{

    public class Location
    {
        public List<Resources> resourceSets { get; set; }
    }
    public class Resources
    {
        public List<Resource> resources { get; set; }
    }

    public class Resource
    {
        public string name { get; set; }
        public string confidence { get; set; }
        public GeoPoint point { get; set; }
    }

    public class GeoPoint
    {
        public decimal[] coordinates { get; set; }
    }
}