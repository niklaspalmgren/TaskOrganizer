using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebStep.Api.Entities;
using WebStep.Api.Services;
using WebStep.Dto;

namespace WebStep.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskBoardsController : ControllerBase
    {
        private readonly ILogger<TaskBoardsController> _logger;
        private readonly IMapper _mapper;
        private readonly ITaskBoardRepo _taskBoardRepo;

        public TaskBoardsController(
            ILogger<TaskBoardsController> logger,
            IMapper mapper,
            ITaskBoardRepo taskBoard)
        {
            _logger = logger;
            _mapper = mapper;
            _taskBoardRepo = taskBoard;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TaskBoardDto>> GetAllTaskBoards()
        {
            var taskBoardsFromRepo = _taskBoardRepo.GetAllTaskBoards();

            var taskBoardDtos = _mapper.Map<IEnumerable<TaskBoard>, IEnumerable<TaskBoardDto>>(taskBoardsFromRepo);

            return Ok(taskBoardDtos);
        }

        [HttpGet("{id}", Name = "GetTaskBoardById")]
        public ActionResult<TaskBoardDto> GetTaskBoardById(int id)
        {
            var taskBoardFromRepo = _taskBoardRepo.GetTaskBoardById(id);

            if (taskBoardFromRepo is null)
                return NotFound();

            var taskBoardDto = _mapper.Map<TaskBoardDto>(taskBoardFromRepo);

            return Ok(taskBoardDto);
        }

        [HttpPost]
        public ActionResult<TaskBoardDto> CreateTaskBoard(TaskBoardDto dto)
        {
            var taskBoard = _mapper.Map<TaskBoard>(dto);

            _taskBoardRepo.CreateTaskBoard(taskBoard);
            _taskBoardRepo.SaveChanges();

            var returnDto = _mapper.Map<TaskBoardDto>(taskBoard);

            return CreatedAtRoute(nameof(GetTaskBoardById), new {Id = returnDto.Id}, returnDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTaskBoard(int id, TaskBoardDto dto)
        {
            var taskBoardEntityFromRepo = _taskBoardRepo.GetTaskBoardById(id);

            if (taskBoardEntityFromRepo == null)
                return NotFound();

            // Updates our entity from db context with values from our incomming dto
            _mapper.Map(dto, taskBoardEntityFromRepo);
            _taskBoardRepo.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTaskBoardAndRelatedTasks(int id)
        {
            var taskBoardEntityFromRepo = _taskBoardRepo.GetTaskBoardById(id);

            if (taskBoardEntityFromRepo == null)
                return NotFound();

            _taskBoardRepo.DeleteTaskBoardAndRelatedTasks(taskBoardEntityFromRepo);
            _taskBoardRepo.SaveChanges();

            return NoContent();
        }
    }
}
