
using System.Collections.Generic;
using TravelListModels;

namespace RestApi.Dtos
{
    public class TravelRouteReadDto
    {
        public int TravelRouteID { get; set; }
        public int TravelListItemID { get; set; }
        public bool Driving { get; set; }
        public int StartTravelPointOfInterestID { get; set; }
        public int EndTravelPointOfInterestID { get; set; }
    }
}
