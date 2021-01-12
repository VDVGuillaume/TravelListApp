namespace RestApi.Dtos
{
    public class TravelTaskListItemCreateDto
    {        
        public string Name { get; set; }
        public bool Checked { get; set; }
        public int TravelListItemID { get; set; }
    }
}
