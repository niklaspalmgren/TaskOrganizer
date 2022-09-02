using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskOrganizer.Api.Entities;
using TaskOrganizer.Api.Services;
using TaskOrganizer.Shared;

namespace TaskOrganizer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskBoardsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITaskBoardRepo _taskBoardRepo;

        public TaskBoardsController(
            IMapper mapper,
            ITaskBoardRepo taskBoard)
        {
            _mapper = mapper;
            _taskBoardRepo = taskBoard;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TaskBoardDto>> GetAllTaskBoards()
        {
            var taskBoardsFromRepo = _taskBoardRepo.GetAllTaskBoards();
            
            if (taskBoardsFromRepo == null || !taskBoardsFromRepo.Any())
                return NotFound();

            var taskBoardDtos = _mapper.Map<IEnumerable<TaskBoard>, IEnumerable<TaskBoardDto>>(taskBoardsFromRepo).ToList();

            return Ok(taskBoardDtos);
        }

        [HttpGet("{id}")]
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

            return CreatedAtAction(nameof(GetTaskBoardById), new { returnDto.Id }, returnDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTaskBoard(int id, TaskBoardDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var taskBoardFromRepo = _taskBoardRepo.GetTaskBoardById(id);

            if (taskBoardFromRepo == null)
                return NotFound();

            // Updates our entity from db context with values from our incomming dto
            _mapper.Map(dto, taskBoardFromRepo);
            _taskBoardRepo.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTaskBoardAndRelatedTasks(int id)
        {
            var taskBoardFromRepo = _taskBoardRepo.GetTaskBoardById(id);

            if (taskBoardFromRepo == null)
                return NotFound();

            _taskBoardRepo.DeleteTaskBoardAndRelatedTasks(taskBoardFromRepo);
            _taskBoardRepo.SaveChanges();

            return NoContent();
        }
    }
}
