using Api.Dtos;
using Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskBoardController : ControllerBase
    {
        private readonly ILogger<TaskBoardController> _logger;
        private readonly IMapper _mapper;
        private readonly ITaskBoardRepo _taskBoardRepo;

        public TaskBoardController(
            ILogger<TaskBoardController> logger,
            IMapper mapper,
            ITaskBoardRepo taskBoard)
        {
            _logger = logger;
            _mapper = mapper;
            _taskBoardRepo = taskBoard;
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskBoardDto>>> GetAllTaskBoards()
        {
            var taskBoards = await _taskBoardRepo.GetAllTaskBoardsAsync();

            var taskBoardDtos = _mapper.Map<List<Entities.TaskBoard>, List<TaskBoardDto>>(taskBoards);

            return Ok(taskBoardDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskBoardDto>> GetTaskBoardByIdAsync(int id)
        {
            var taskBoard = await _taskBoardRepo.GetTaskBoardByIdAsync(id);

            if (taskBoard is null)
                return BadRequest("No task board found.");

            var taskBoardDto = _mapper.Map<TaskBoardDto>(taskBoard);

            return Ok(taskBoardDto);
        }
    }
}
