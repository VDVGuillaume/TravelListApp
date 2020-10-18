using System;
using System.Collections.Generic;
using System.Text;

namespace TravelListRepository.Rest
{
    class RestTravelListInstanceRepository
    {
        private readonly string _url;

        public RestTravelListInstanceRepository(string url)
        {
            _url = url;
        }

        public ITravelListRepo TravelLists => new RestTravelListRepository(_url);
    }
}
