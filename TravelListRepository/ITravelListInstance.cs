using System;
using System.Collections.Generic;
using System.Text;

namespace TravelListRepository
{
    public interface ITravelListInstance
    {
        ITravelListRepo TravelLists { get; }
    }
}
