namespace RestApi.Dtos
{
    public class CheckListItemReadDto
    {
        public int CheckListItemID { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
        public int TravelListItemID { get; set; }
    }
}
