
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
    public class TravelPointOfInterestsController : ControllerBase
    {
        private readonly ITravelPointOfInterestRepo _repo;
        private readonly IMapper _mapper;

        public TravelPointOfInterestsController(ITravelPointOfInterestRepo repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }

        //api/TravelPointOfInterests/{id}
        [HttpGet("{id}", Name = "GetTravelPointOfInterestById")]
        public async Task<IActionResult> GetTravelPointOfInterestById(int id)
        {
            var travelPointOfInterest = await _repo.GetTravelPointOfInterestById(id);
            if (travelPointOfInterest != null)
            {
                return Ok(_mapper.Map<TravelPointOfInterestReadDto>(travelPointOfInterest));
            }
            return NotFound();
        }

        //api/TravelPointOfInterests
        [HttpPost]
        public async Task<IActionResult> CreateTravelPointOfInterest([FromBody]TravelPointOfInterestCreateDto travelPointOfInterestCreateDto)
        {
            var travelPointOfInterestModel = _mapper.Map<TravelPointOfInterest>(travelPointOfInterestCreateDto);
            await _repo.CreateTravelPointOfInterest(travelPointOfInterestModel);
            _repo.SaveChanges();

            var travelPointOfInterestReadDto = _mapper.Map<TravelPointOfInterestReadDto>(travelPointOfInterestModel);

            return CreatedAtRoute(nameof(GetTravelPointOfInterestById), new { Id = travelPointOfInterestReadDto.TravelPointOfInterestID }, travelPointOfInterestReadDto);
        }

        //DELETE api/TravelPointOfInterests/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTravelPointOfInterest(int id)
        {
            var travelPointOfInterestModel = await _repo.GetTravelPointOfInterestById(id);
            if (travelPointOfInterestModel == null)
            {
                return NotFound();
            }

            await _repo.DeleteTravelPointOfInterest(travelPointOfInterestModel);

            _repo.SaveChanges();

            return NoContent();
        }

        //api/TravelPointOfInterests/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTravelList(int id, [FromBody]TravelPointOfInterestCreateDto travelPointOfInterestUpdateDto)
        {
            var travelPointOfInterestModel = await _repo.GetTravelPointOfInterestById(id);
            if (travelPointOfInterestModel == null)
            {
                return NotFound();
            }

            _mapper.Map(travelPointOfInterestUpdateDto, travelPointOfInterestModel);

            await _repo.UpdateTravelPointOfInterest(id, travelPointOfInterestModel);

            _repo.SaveChanges();

            return NoContent();

        }

    }
}
