﻿
namespace RestApi.Dtos
{
    public class TravelListReadDto
    {
        public int TravelListItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Country { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
