using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelListModels;
using TravelListRepository.Rest;

namespace TravelListRepository.Restcountries
{
    public class RestCountriesRepo : ICountryRepo
    {
        private readonly HttpHelper _http = new HttpHelper("https://restcountries.eu/rest/v2/all");
        public async Task<IEnumerable<Country>> GetAllCountries() =>
            await _http.GetAsync<IEnumerable<Country>>("");

    }
}
