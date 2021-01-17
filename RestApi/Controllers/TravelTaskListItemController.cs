
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestApi.Dtos;
using System.Collections;
using System.Threading.Tasks;
using TravelListModels;
using TravelListRepository;

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class TravelTaskListItemController : ControllerBase
    {
        private readonly ITaskListItemRepo _repo;
        private readonly IMapper _mapper;

        public TravelTaskListItemController(ITaskListItemRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskListItem([FromBody]TravelTaskListItemCreateDto taskListItemCreateDto)
        {
            var taskListItem = _mapper.Map<TravelTaskListItem>(taskListItemCreateDto);
            await _repo.CreateTaskListItemAsync(taskListItem);
            _repo.SaveChanges();

            var tasklistReadDto = _mapper.Map<TravelTaskListItemReadDto>(taskListItem);


            return CreatedAtRoute(nameof(GetTaskListItemById), new { Id = tasklistReadDto.TravelTaskListItemID }, tasklistReadDto);


        }

        [HttpGet("GetTasklist")]
        public async Task<IActionResult> GetTaskList(int value)
        {
            var taskListItems = await _repo.GetTaskList(value);
            return Ok(_mapper.Map<IEnumerable>(taskListItems));

        }


        [HttpDelete("{id}")]
        public async Task DeleteTaskListItem(int id)
        {
            var TaskListItemFromRepo = await _repo.GetTaskListItemById(id);
            await _repo.DeleteTaskListItemAsync(TaskListItemFromRepo);

            _repo.SaveChanges();

        }

        [HttpPut("{id}")]
        public async Task UpdateTaskListItem(int id, [FromBody]TravelTaskListItemCreateDto taskListUpdateDto)
        {
            var TaskListItemFromRepo = await _repo.GetTaskListItemById(id);
            _mapper.Map(taskListUpdateDto, TaskListItemFromRepo);

            await _repo.UpdateTaskListItemAsync(id, TaskListItemFromRepo);

            _repo.SaveChanges();


        }

        [HttpGet("{id}", Name = "GetTaskListItemById")]
        public async Task<IActionResult> GetTaskListItemById(int id)
        {
            var travelListItem = await _repo.GetTaskListItemById(id);
            if (travelListItem != null)
            {
                return Ok(_mapper.Map<TravelListReadDto>(travelListItem));
            }
            return NotFound();
        }





    }
}