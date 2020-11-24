
using System.Collections.Generic;
using TravelListModels;

namespace RestApi.Dtos
{
    public class TravelListImageReadDto
    {
        public int TravelListItemImageID { get; set; }
        public int TravelListItemID { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageName { get; set; }
    }
}
