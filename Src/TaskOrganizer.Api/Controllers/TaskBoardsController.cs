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

        // api/tasks
        [HttpGet]
        public async Task<ActionResult<List<TaskBoardDto>>> GetTaskBoards()
        {
            var taskBoardsFromRepo = await _taskBoardRepo.GetTaskBoardsAsync();
            
            if (taskBoardsFromRepo == null || !taskBoardsFromRepo.Any())
                return NotFound();

            var taskBoardDtos = _mapper.Map<List<TaskBoard>, List<TaskBoardDto>>(taskBoardsFromRepo);

            return Ok(taskBoardDtos);
        }

        // api/tasks/3
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskBoardDto>> GetTaskBoardById(int id)
        {
            var taskBoardFromRepo = await _taskBoardRepo.GetTaskBoardByIdAsync(id);

            if (taskBoardFromRepo is null)
                return NotFound();                

            var taskBoardDto = _mapper.Map<TaskBoardDto>(taskBoardFromRepo);

            return Ok(taskBoardDto);
        }

        // api/tasks
        [HttpPost]
        public async Task<ActionResult<TaskBoardDto>> CreateTaskBoard(TaskBoardDto dto)
        {
            var taskBoard = _mapper.Map<TaskBoard>(dto);

            _taskBoardRepo.AddTaskBoard(taskBoard);
            await _taskBoardRepo.SaveChangesAsync();

            var returnDto = _mapper.Map<TaskBoardDto>(taskBoard);

            return CreatedAtAction(nameof(GetTaskBoardById), new { returnDto.Id }, returnDto);
        }

        // api/tasks/3
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTaskBoard(int id, TaskBoardDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var taskBoardFromRepo = await _taskBoardRepo.GetTaskBoardByIdAsync(id);

            if (taskBoardFromRepo == null)
                return NotFound();

            // Updates our entity from db context with values from our incoming dto
            _mapper.Map(dto, taskBoardFromRepo);
            await _taskBoardRepo.SaveChangesAsync();

            return NoContent();
        }

        // api/tasks/3
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTaskBoardAndRelatedTasks(int id)
        {
            var taskBoardFromRepo = await _taskBoardRepo.GetTaskBoardByIdAsync(id);

            if (taskBoardFromRepo == null)
                return NotFound();

            await _taskBoardRepo.DeleteTaskBoardAndRelatedTasksAsync(taskBoardFromRepo);
            await _taskBoardRepo.SaveChangesAsync();

            return NoContent();
        }
    }
}
