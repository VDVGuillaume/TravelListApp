using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelListModels;

namespace TravelListApp.Models
{
    public class CheckListItem : TravelCheckListItem
    {
        public bool IsNew { get; set; }
        public bool ToRemove { get; set; }
    }
}
