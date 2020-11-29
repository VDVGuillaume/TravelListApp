using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelListModels;
using Windows.Devices.Geolocation;
using Windows.Foundation;

namespace TravelListApp.Models
{
    public class ListItemImage : TravelListItemImage
    {
        public Boolean IsNew { get; set; }
        public Boolean ToRemove { get; set; }
    }
}
