
using System.Collections.Generic;
using TravelListModels;

namespace RestApi.Dtos
{
    public class TravelPointOfInterestReadDto
    {
        public int TravelPointOfInterestID { get; set; }
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int TravelListItemID { get; set; }
    }
}
