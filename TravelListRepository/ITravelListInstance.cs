using System;
using System.Collections.Generic;
using System.Text;

namespace TravelListRepository
{
    public interface ITravelListInstance
    {
        ITravelListItemRepo TravelLists { get; }
        ICountryRepo Countries { get; }
    }
}
