
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
                return Ok(_mapper.Map<TravelChecklistItemReadDto>(travelPointOfInterest));
            }
            return NotFound();
        }

        //api/travellists
        [HttpPost]
        public async Task<IActionResult> CreateTravelPointOfInterest([FromBody]TravelPointOfInterestCreateDto travelPointOfInterestCreateDto)
        {
            var travelPointOfInterestModel = _mapper.Map<TravelPointOfInterest>(travelPointOfInterestCreateDto);
            await _repo.CreateTravelPointOfInterest(travelPointOfInterestModel);
            _repo.SaveChanges();

            var travelPointOfInterestReadDto = _mapper.Map<TravelChecklistItemReadDto>(travelPointOfInterestModel);

            return CreatedAtRoute(nameof(GetTravelPointOfInterestById), new { Id = travelPointOfInterestReadDto.TravelPointOfInterestID }, travelPointOfInterestReadDto);
        }

        ////api/travellists/{id}
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateTravelList(int id, [FromBody]TravelListCreateDto travelListUpdateDto)
        //{
        //    var travelListModelFromRepo = await _repo.GetTravelListById(id);
        //    if (travelListModelFromRepo == null)
        //    {
        //        return NotFound();
        //    }

        //    _mapper.Map(travelListUpdateDto, travelListModelFromRepo);

        //    await _repo.UpdateTravelList(id, travelListModelFromRepo);

        //    _repo.SaveChanges();

        //    return NoContent();

        //}

        ////DELETE api/travellists/{id}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTravelList(int id)
        //{
        //    var travelListModelFromRepo = await _repo.GetTravelListById(id);
        //    if (travelListModelFromRepo == null)
        //    {
        //        return NotFound();
        //    }

        //    await _repo.DeleteTravelList(travelListModelFromRepo);

        //    _repo.SaveChanges();

        //    return NoContent();
        //}

    }
}
