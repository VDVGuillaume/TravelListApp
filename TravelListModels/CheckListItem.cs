namespace TravelListModels
{
    public class CheckListItem
    {
        public int CheckListItemID { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
        public int TravelListItemID { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
