
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestApi.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelListModels;
using TravelListRepository;

namespace RestApi.Controllers
{
    //api/country
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryRepo _repo;
        private readonly IMapper _mapper;

        public CountriesController(ICountryRepo repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }

        //api/country
        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            var countries = await _repo.GetAllCountries();
            if (countries is null)
            {
                countries = GetAllCountriesOffline();
            }
            return Ok(_mapper.Map<IEnumerable<CountryReadDto>>(countries));
        }

        public IEnumerable<Country> GetAllCountriesOffline()
        {
            string allText = System.IO.File.ReadAllText(@"Countries.json");

            IEnumerable<Country> jsonObject = JsonConvert.DeserializeObject<IEnumerable<Country>>(allText);
            return jsonObject;
        }

    }
}
