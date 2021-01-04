using System;
using System.Collections.Generic;
using System.Text;

namespace TravelListRepository
{
    public interface ITravelListInstance
    {
        ITravelListItemRepo TravelLists { get; }
        ITravelPointOfInterestRepo Points { get; }
        ITravelListItemImageRepo TravelListImages { get; }
        ITravelRouteRepo Routes { get; }
        ICountryRepo Countries { get; }
        IBingRepo Bing { get; }
    }
}
