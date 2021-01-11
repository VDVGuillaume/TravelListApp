using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestApi.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelListModels;
using TravelListRepository;

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelCheckListItemController : ControllerBase
    {
        private readonly ICheckListItemRepo _repo;
        private readonly IMapper _mapper;

        public TravelCheckListItemController(ICheckListItemRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        //api/travellists
        [HttpPost]
        public async Task CreateCheckListItem([FromBody]TravelCheckListItemCreateDto checkListItemCreateDto)
        {
            var checkListItem = _mapper.Map<TravelCheckListItem>(checkListItemCreateDto);
            await _repo.CreateCheckListItemAsync(checkListItem);
            _repo.SaveChanges();
            

        }

        //api/travellistimages/{id}
        [HttpGet("{travelListId}", Name = "CheckList")]
        public async Task<IActionResult> GetCheckList(int travelListId) {

            var checkList = await _repo.GetCheckList(travelListId);
            return Ok(_mapper.Map<IEnumerable<ChecklistItemReadDto>>(checkList));

        }


        //DELETE api/travellistimages/{id}
        [HttpDelete("{id}")]
        public async Task DeleteCheckListItem(int id)
        {
            var CheckListItemFromRepo = await _repo.GetCheckListItemById(id);          
            await _repo.DeleteCheckListItemAsync(CheckListItemFromRepo);

            _repo.SaveChanges();

        }

        [HttpPut("{id}")]
        public async Task UpdateCheckListItem(int id, [FromBody]TravelCheckListItemCreateDto checkListUpdateDto)
        {
            var CheckListItemFromRepo = await _repo.GetCheckListItemById(id);            
            _mapper.Map(checkListUpdateDto, CheckListItemFromRepo);

            await _repo.UpdateCheckListItemAsync(id, CheckListItemFromRepo);

            _repo.SaveChanges();
           

        }




    }
}