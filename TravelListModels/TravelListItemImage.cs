using System;
using System.Collections.Generic;
using System.Text;

namespace TravelListModels
{
    public class TravelListItemImage
    {
        public int TravelListItemImageID { get; set; }
        public int TravelListItemID { get; set; }
        public byte[] ImageData { get; set; }
    }
}
