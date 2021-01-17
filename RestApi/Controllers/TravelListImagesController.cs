
using System.Collections.Generic;
using AutoMapper;
using RestApi.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TravelListRepository;
using TravelListModels;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;

namespace RestApi.Controllers
{
    //api/travellistimages
    [Route("api/[controller]")]
    public class TravelListImagesController : ControllerBase
    {
        private readonly ITravelListItemImageRepo _repo;
        private readonly IMapper _mapper;

        public TravelListImagesController(ITravelListItemImageRepo repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }

        //api/travellistimages/{id}
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

        //api/travellistimages/{id}
        [HttpPost("{id}")]
        public async Task<IActionResult> CreateTravelListImage(int id, [FromForm]TravelListImageCreateDto travelListCreateDto)
        {
            using (var ms = new MemoryStream())
            {
                Request.Form.Files[0].CopyTo(ms);
                var fileBytes = ms.ToArray();
                travelListCreateDto.ImageData = fileBytes;
            }
            var travelListItemImageModel = _mapper.Map<TravelListItemImage>(travelListCreateDto);
            await _repo.CreateTravelListImage(travelListItemImageModel);
            _repo.SaveChanges();

            var travelListImageReadDto = _mapper.Map<TravelListImageReadDto>(travelListItemImageModel);

            return CreatedAtRoute(nameof(GetTravelListImageById), new { Id = travelListImageReadDto.TravelListItemImageID }, travelListImageReadDto);
        }

        //DELETE api/travellistimages/{id}
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
