namespace RestApi.Dtos
{
    public class TravelCheckListItemCreateDto
    {
        public int TravelListItemID { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public string Category { get; set; }       
        public bool Checked { get; set; }
        
    }
}
