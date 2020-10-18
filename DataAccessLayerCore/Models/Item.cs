using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerCore.Models
{
    public class Item
    {
        public int ItemID { get; set; }
        public string Name { get; set; }
        public int TravelListID { get; set; }
        public virtual TravelList TravelList { get; set; }
    }
}
