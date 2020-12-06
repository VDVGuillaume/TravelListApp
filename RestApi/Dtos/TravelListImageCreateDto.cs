
namespace RestApi.Dtos
{
    public class TravelListImageCreateDto
    {
        public int TravelListItemID { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageName { get; set; }
    }
}