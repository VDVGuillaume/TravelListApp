using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestApi.Dtos;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TravelListModels;
using TravelListRepository;

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckListItemController : ControllerBase
    {
        private readonly ICheckListItemRepo _repo;
        private readonly IMapper _mapper;

        public CheckListItemController(ICheckListItemRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        //api/travellists
        [HttpPost]
        public async Task<IActionResult> CreateCheckListItem([FromBody]CheckListItemCreateDto checkListItemCreateDto)
        {
            var checkListItem = _mapper.Map<CheckListItem>(checkListItemCreateDto);
            await _repo.CreateCheckListItem(checkListItem);
            _repo.SaveChanges();

            var checkListItemReadDto = _mapper.Map<ChecklistItemReadDto>(checkListItem);

            return CreatedAtRoute(nameof(GetCheckList), new { Id = checkListItemCreateDto.CheckListItemID }, checkListItemReadDto);
        }

        //api/travellistimages/{id}
        [HttpGet("{travelListId}", Name = "CheckList")]
        public async Task<IActionResult> GetCheckList(int travelListId) {

            var checkList = await _repo.GetCheckList(travelListId);
            return Ok(_mapper.Map<IEnumerable<ChecklistItemReadDto>>(checkList));

        }


    }
}