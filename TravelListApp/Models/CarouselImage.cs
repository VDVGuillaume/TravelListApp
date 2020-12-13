using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelListApp.Services.Icons;
using TravelListApp.ViewModels;
using TravelListModels;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.UI.Xaml.Media.Imaging;

namespace TravelListApp.Models
{
    public class CarouselImage : TravelListItemImage
    {
        public CarouselImage(TravelListItemImage image)
        {
            TravelListItemImageID = image.TravelListItemImageID;
            TravelListItemID = image.TravelListItemID;
            ImageData = image.ImageData;
            ImageName = image.ImageName;
        }
        public WriteableBitmap Photo { get; set; }
        public ButtonItem DeleteIcon { get; } = new ButtonItem() { Glyph = Icon.GetIcon("Clear"), Text = "Clear" };
        public Boolean IsNew { get; set; }
        public Boolean ToRemove { get; set; }
    }
}
