using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelListModels;
using TravelListRepository.Rest;

namespace TravelListRepository.RestBing
{
    public class RestBingRepo : IBingRepo
    {
        private readonly HttpHelper _http = new HttpHelper("http://dev.virtualearth.net/REST/v1/Locations?");
        private readonly string key = System.Environment.GetEnvironmentVariable("MAP_SERVICE_TOKEN");
        public async Task<Location> GetLocationByQuery(string search) =>
            await _http.GetAsync<Location>("",$"q={search}&key={key}");

    }
}
