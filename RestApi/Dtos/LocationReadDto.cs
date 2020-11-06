
using System.Collections.Generic;
using Windows.Data.Json;

namespace RestApi.Dtos
{

    public class LocationReadDto
    {
        public List<ResourcesReadDto> resourceSets { get; set; }
    }
    public class ResourcesReadDto
    {
        public List<ResourceReadDto> resources { get; set; }
    }

    public class ResourceReadDto
    {
        public string name { get; set; }
        public string confidence { get; set; }
        public GeoPointReadDto point { get; set; }
    }

    public class GeoPointReadDto
    {
        public decimal[] coordinates { get; set; }
    }
}
