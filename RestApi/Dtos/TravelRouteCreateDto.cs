
namespace RestApi.Dtos
{
    public class TravelRouteCreateDto
    {
        public int TravelRouteID { get; set; }
        public int TravelListItemID { get; set; }
        public int StartTravelPointOfInterestID { get; set; }
        public int EndTravelPointOfInterestID { get; set; }
    }
}