
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
    public class TravelRoutesController : ControllerBase
    {
        private readonly ITravelRouteRepo _repo;
        private readonly IMapper _mapper;

        public TravelRoutesController(ITravelRouteRepo repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }

        //api/TravelRoutes/{id}
        [HttpGet("{id}", Name = "GetTravelRouteById")]
        public async Task<IActionResult> GetTravelRouteById(int id)
        {
            var travelRoute = await _repo.GetTravelRouteById(id);
            if (travelRoute != null)
            {
                return Ok(_mapper.Map<TravelRouteReadDto>(travelRoute));
            }
            return NotFound();
        }

        //api/TravelRoutes
        [HttpPost]
        public async Task<IActionResult> CreateTravelRoute([FromBody]TravelRouteCreateDto travelRouteCreateDto)
        {
            var travelRouteModel = _mapper.Map<TravelRoute>(travelRouteCreateDto);
            await _repo.CreateTravelRoute(travelRouteModel);
            _repo.SaveChanges();

            var travelRouteReadDto = _mapper.Map<TravelRouteReadDto>(travelRouteModel);

            return CreatedAtRoute(nameof(GetTravelRouteById), new { Id = travelRouteReadDto.TravelRouteID }, travelRouteReadDto);
        }

        //DELETE api/TravelRoutes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTravelRoute(int id)
        {
            var travelRouteModel = await _repo.GetTravelRouteById(id);
            if (travelRouteModel == null)
            {
                return NotFound();
            }

            await _repo.DeleteTravelRoute(travelRouteModel);

            _repo.SaveChanges();

            return NoContent();
        }

        //api/TravelRoutes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTravelRoute(int id, [FromBody]TravelRouteCreateDto travelRouteUpdateDto)
        {
            var travelRouteModel = await _repo.GetTravelRouteById(id);
            if (travelRouteModel == null)
            {
                return NotFound();
            }

            _mapper.Map(travelRouteUpdateDto, travelRouteModel);

            await _repo.UpdateTravelRoute(id, travelRouteModel);

            _repo.SaveChanges();

            return NoContent();

        }

    }
}
