
using System.Collections.Generic;
using AutoMapper;
using RestApi.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TravelListRepository;
using TravelListModels;

namespace RestApi.Controllers
{
    //api/travellists
    [Route("api/[controller]")]
    //[Route("api/travellists")]
    //[ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryRepo _repo;
        private readonly IMapper _mapper;

        public CountriesController(ICountryRepo repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }

        //api/travellists
        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {


            var travelListItems = await _repo.GetAllCountries();

            return Ok(_mapper.Map<IEnumerable<CountryReadDto>>(travelListItems));
        }

    }
}
