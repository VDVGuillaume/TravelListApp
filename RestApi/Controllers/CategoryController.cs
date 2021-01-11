
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
    [Route("api/[controller]")]
    //[ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepo _repo;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepo repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }

               
        
        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories(string value)
        {            
            var categories = await _repo.GetAllCategories(value);
            return Ok(_mapper.Map<IEnumerable<CategoryReadDto>>(categories));
        }

        
        [HttpPost]
        public async Task CreateCategory([FromBody]CategoryCreateDto CategoryCreateDto)
        {
            var categoryModel = _mapper.Map<Category>(CategoryCreateDto);
            await _repo.CreateCategory(categoryModel);
            _repo.SaveChanges();

        }




    }
}