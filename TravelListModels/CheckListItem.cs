using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelListModels
{
    public class CheckListItem
    {
        public int CheckListItemID { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
        public int TravelListItemID { get; set; }
        // public virtual TravelListItem TravelListItem { get; set; }
    }
}
