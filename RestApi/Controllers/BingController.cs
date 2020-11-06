
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
    public class BingController : ControllerBase
    {
        private readonly IBingRepo _repo;
        private readonly IMapper _mapper;

        public BingController(IBingRepo repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }

        //api/travellists
        [HttpGet("{search}", Name = "GetLocationByQuery")]
        public async Task<IActionResult> GetLocationByQuery(string search)
        {

            var locations = await _repo.GetLocationByQuery(search);

            return Ok(_mapper.Map<LocationReadDto>(locations));
        }

    }
}
