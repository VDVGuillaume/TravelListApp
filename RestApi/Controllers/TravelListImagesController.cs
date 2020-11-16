
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
    public class TravelListImagesController : ControllerBase
    {
        private readonly ITravelListItemImageRepo _repo;
        private readonly IMapper _mapper;

        public TravelListImagesController(ITravelListItemImageRepo repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }

        //api/travellists/{id}
        [HttpGet("{id}", Name = "GetTravelListImageById")]
        public async Task<IActionResult> GetTravelListImageById(int id)
        {
            var travelListImageItem = await _repo.GetTravelListImageById(id);
            if (travelListImageItem != null)
            {
                return Ok(_mapper.Map<TravelListImageReadDto>(travelListImageItem));
            }
            return NotFound();
        }

        //api/travellists
        [HttpPost]
        public async Task<IActionResult> CreateTravelListImage([FromBody]TravelListImageCreateDto travelListCreateDto)
        {
            var travelListItemImageModel = _mapper.Map<TravelListItemImage>(travelListCreateDto);
           await _repo.CreateTravelListImage(travelListItemImageModel);
            _repo.SaveChanges();

            var travelListImageReadDto = _mapper.Map<TravelListImageReadDto>(travelListItemImageModel);

            return CreatedAtRoute(nameof(GetTravelListImageById), new { Id = travelListImageReadDto.TravelListItemImageID }, travelListImageReadDto);
        }

        //api/travellists/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTravelListImage(int id, [FromBody]TravelListImageCreateDto travelListUpdateDto)
        {
            var travelListItemImageModelFromRepo = await _repo.GetTravelListImageById(id);
            if (travelListItemImageModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(travelListUpdateDto, travelListItemImageModelFromRepo);

            await _repo.UpdateTravelListImage(id, travelListItemImageModelFromRepo);

            _repo.SaveChanges();

            return NoContent();

        }

        //DELETE api/travellists/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTravelListImage(int id)
        {
            var travelListItemImageModelFromRepo = await _repo.GetTravelListImageById(id);
            if (travelListItemImageModelFromRepo == null)
            {
                return NotFound();
            }

            await _repo.DeleteTravelListImage(travelListItemImageModelFromRepo);

            _repo.SaveChanges();

            return NoContent();
        }

    }
}
