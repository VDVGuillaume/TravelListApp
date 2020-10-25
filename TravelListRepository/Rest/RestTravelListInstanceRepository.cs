using System;
using System.Collections.Generic;
using System.Text;

namespace TravelListRepository.Rest
{
    public class RestTravelListInstanceRepository : ITravelListInstance
    {
        private readonly string _url;

        public RestTravelListInstanceRepository(string url)
        {
            _url = url;
        }

        public ITravelListItemRepo TravelLists => new RestTravelListRepository(_url);
    }
}
