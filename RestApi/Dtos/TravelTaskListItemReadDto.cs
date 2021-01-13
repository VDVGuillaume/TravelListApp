namespace RestApi.Dtos
{
    public class TravelTaskListItemReadDto
    {

        public int TravelTaskListItemID { get; set; }
        public string Name { get; set; }      
        public bool Checked { get; set; }
        public int TravelListItemID { get; set; }
    }
}
