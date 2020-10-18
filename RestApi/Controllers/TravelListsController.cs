
using System.Collections.Generic;
using AutoMapper;
using RestApi.Dtos;
using DataAccessLayerCore.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayerCore.Data;

namespace RestApi.Controllers
{
    //api/travellists
    //[Route("api/[controller]")]
    [Route("api/travellists")]
    [ApiController]
    public class TravelListsController : ControllerBase
    {
        private readonly ITravelListRepo _repo;
        private readonly IMapper _mapper;

        public TravelListsController(ITravelListRepo repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }

        //api/travellists
        [HttpGet]
        public ActionResult<IEnumerable<TravelListReadDto>> GetAllTravelLists()
        {
            var travelListItems = _repo.GetAllTravelLists();

            return Ok(_mapper.Map<IEnumerable<TravelListReadDto>>(travelListItems));
        }

        //api/travellists/{id}
        [HttpGet("{id}", Name = "GetTravelListById")]
        public ActionResult<TravelListReadDto> GetTravelListById(int id)
        {
            var travelListItem = _repo.GetTravelListById(id);
            if (travelListItem != null)
            {
                return Ok(_mapper.Map<TravelListReadDto>(travelListItem));
            }
            return NotFound();
        }

        //api/travellists
        [HttpPost]
        public ActionResult<TravelListReadDto> CreateTravelList(TravelListCreateDto travelListCreateDto)
        {
            var travelListModel = _mapper.Map<TravelList>(travelListCreateDto);
            _repo.CreateTravelList(travelListModel);
            _repo.SaveChanges();

            var travelListReadDto = _mapper.Map<TravelListReadDto>(travelListModel);

            return CreatedAtRoute(nameof(GetTravelListById), new { Id = travelListReadDto.TravelListID }, travelListReadDto);
        }

        //api/travellists/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateTravelList(int id, TravelListCreateDto travelListUpdateDto)
        {
            var travelListModelFromRepo = _repo.GetTravelListById(id);
            if (travelListModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(travelListUpdateDto, travelListModelFromRepo);

            _repo.UpdateTravelList(travelListModelFromRepo);

            _repo.SaveChanges();

            return NoContent();

        }

        //PATCH api/travellists/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialTravelListUpdate(int id, JsonPatchDocument<TravelListUpdateDto> patchDoc)
        {
            var travelListModelFromRepo = _repo.GetTravelListById(id);
            if (travelListModelFromRepo == null)
            {
                return NotFound();
            }
            var travelListToPatch = _mapper.Map<TravelListUpdateDto>(travelListModelFromRepo);
            patchDoc.ApplyTo(travelListToPatch, ModelState);

            if (!TryValidateModel(travelListToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(travelListToPatch, travelListModelFromRepo);

            _repo.UpdateTravelList(travelListModelFromRepo);

            _repo.SaveChanges();

            return NoContent();

        }

        //DELETE api/travellists/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteTravelList(int id)
        {
            var travelListModelFromRepo = _repo.GetTravelListById(id);
            if (travelListModelFromRepo == null)
            {
                return NotFound();
            }

            _repo.DeleteTravelList(travelListModelFromRepo);

            _repo.SaveChanges();

            return NoContent();
        }

    }
}
