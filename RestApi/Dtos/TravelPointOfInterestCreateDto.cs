﻿
namespace RestApi.Dtos
{
    public class TravelPointOfInterestCreateDto
    {
        public int TravelPointOfInterestID { get; set; }
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int TravelListItemID { get; set; }
    }
}