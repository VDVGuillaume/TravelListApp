namespace TravelListModels
{
    public class TravelCheckListItem
    {
        public int TravelCheckListItemID { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public string Category { get; set; }    
        public bool Checked { get; set; }
        public int TravelListItemID { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
