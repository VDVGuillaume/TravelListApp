using System;
using System.Collections.Generic;
using System.Text;

namespace TravelListModels
{
    public class TravelTaskListItem
    {
        public int TravelTaskListItemID { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
        public int TravelListItemID { get; set; }

        public override string ToString()
        {
            return Name;
        }

    }
}
