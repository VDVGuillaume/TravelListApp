
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
    public class TravelListsController : ControllerBase
    {
        private readonly ITravelListItemRepo _repo;
        private readonly IMapper _mapper;

        public TravelListsController(ITravelListItemRepo repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }

        //api/travellists
        [HttpGet("GetAllTravelLists")]
        public async Task<IActionResult> GetAllTravelLists(string value)
        {
            var travelListItems = await _repo.GetAllTravelLists(value);
            return Ok(_mapper.Map<IEnumerable<TravelListReadDto>>(travelListItems));
        }

        //api/travellists
        [HttpGet("GetFirstUpcomingTravelList")]
        public async Task<IActionResult> GetFirstUpcomingTravelList(string value)
        {
            var travelListItem = await _repo.GetFirstUpcomingTravelList(value);
            if (travelListItem != null)
            {
                return Ok(_mapper.Map<TravelListReadDto>(travelListItem));
            }
            return NotFound();
        }

        //api/travellists/{id}
        [HttpGet("{id}", Name = "GetTravelListById")]
        public async Task<IActionResult> GetTravelListById(int id)
        {
            var travelListItem = await _repo.GetTravelListById(id);
            if (travelListItem != null)
            {
                return Ok(_mapper.Map<TravelListReadDto>(travelListItem));
            }
            return NotFound();
        }

        //api/travellists
        [HttpPost]
        public async Task<IActionResult> CreateTravelList([FromBody]TravelListCreateDto travelListCreateDto)
        {
            var travelListModel = _mapper.Map<TravelListItem>(travelListCreateDto);
           await _repo.CreateTravelList(travelListModel);
            _repo.SaveChanges();

            var travelListReadDto = _mapper.Map<TravelListReadDto>(travelListModel);

            return CreatedAtRoute(nameof(GetTravelListById), new { Id = travelListReadDto.TravelListItemID }, travelListReadDto);
        }

        //api/travellists/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTravelList(int id, [FromBody]TravelListCreateDto travelListUpdateDto)
        {
            var travelListModelFromRepo = await _repo.GetTravelListById(id);
            if (travelListModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(travelListUpdateDto, travelListModelFromRepo);

            await _repo.UpdateTravelList(id, travelListModelFromRepo);

            _repo.SaveChanges();

            return NoContent();

        }

        //DELETE api/travellists/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTravelList(int id)
        {
            var travelListModelFromRepo = await _repo.GetTravelListById(id);
            if (travelListModelFromRepo == null)
            {
                return NotFound();
            }

            await _repo.DeleteTravelList(travelListModelFromRepo);

            _repo.SaveChanges();

            return NoContent();
        }

    }
}
