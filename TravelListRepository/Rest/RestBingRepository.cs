using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelListModels;

namespace TravelListRepository.Rest
{
    class RestBingRepository : IBingRepo
    {
        private readonly HttpHelper _http;

        public RestBingRepository(string baseUrl)
        {
            _http = new HttpHelper(baseUrl);
        }

        public async Task<Location> GetLocationByQuery(string search) =>
            await _http.GetAsync<Location>($"bing/{search}");

    }
}
