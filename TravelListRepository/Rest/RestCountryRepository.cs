using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelListModels;

namespace TravelListRepository.Rest
{
    class RestCountryRepository : ICountryRepo
    {
        private readonly HttpHelper _http;

        public RestCountryRepository(string baseUrl)
        {
            _http = new HttpHelper(baseUrl);
        }

        public async Task<IEnumerable<Country>> GetAllCountries() =>
            await _http.GetAsync<IEnumerable<Country>>("countries");

    }
}
