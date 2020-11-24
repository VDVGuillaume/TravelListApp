
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

        //api/travellists
        [HttpGet]
        public async Task<IActionResult> GetAllTravelListImages()
        {


            var travelListImageItems = await _repo.GetAllTravelListImages();

            return Ok(_mapper.Map<IEnumerable<TravelListImageReadDto>>(travelListImageItems));
        }

        //api/travellistimages/{id}/imagedata
        [HttpGet("{id}/imagedata", Name = "GetTravelListImageDataById")]
        [Produces("application/octet-stream")]
        public async Task<FileStreamResult> GetTravelListImageDataById(int id)
        {
            byte[] imageData = await _repo.GetTravelListImageDataById(id);
            return File(new MemoryStream(imageData), "application/octet-stream");

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
                // string s = Convert.ToBase64String(fileBytes);
                // act on the Base64 data
            }
            //using (var reader = new StreamReader(Request.Form.Files[0].OpenReadStream()))
            //{
            //    string contentAsString = reader.ReadToEnd();
            //    byte[] bytes = new byte[contentAsString.Length * sizeof(char)];
            //    System.Buffer.BlockCopy(contentAsString.ToCharArray(), 0, bytes, 0, bytes.Length);
            //    travelListCreateDto.ImageData = bytes;
            //}
            var travelListItemImageModel = _mapper.Map<TravelListItemImage>(travelListCreateDto);
            await _repo.CreateTravelListImage(travelListItemImageModel);
            _repo.SaveChanges();

            var travelListImageReadDto = _mapper.Map<TravelListImageReadDto>(travelListItemImageModel);

            return CreatedAtRoute(nameof(GetTravelListImageById), new { Id = travelListImageReadDto.TravelListItemImageID }, travelListImageReadDto);
        }

        //api/travellistimages/{id}
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
