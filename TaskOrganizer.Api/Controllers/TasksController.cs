using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskOrganizer.Api.Services;
using TaskOrganizer.Shared;

namespace TaskOrganizer.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly ILogger<TasksController> _logger;
    private readonly IMapper _mapper;
    private readonly ITaskRepo _taskRepo;

    public TasksController(
        ILogger<TasksController> logger,
        IMapper mapper,
        ITaskRepo taskRepo)
    {
        _logger = logger;
        _mapper = mapper;
        _taskRepo = taskRepo;
    }


    [HttpGet]
    public ActionResult<IEnumerable<TaskDto>> GetAllTasks()
    {
        var tasks = _taskRepo.GetAllTasks();

        var taskDtos = _mapper.Map<IEnumerable<Entities.Task>, IEnumerable<TaskDto>>(tasks);

        return Ok(taskDtos);
    }

    [HttpGet("{id}")]
    public ActionResult<TaskDto> GetTaskById(int id)
    {
        var taskItem = _taskRepo.GetTaskById(id);

        if (taskItem == null)
            return NotFound();

        var taskDto = _mapper.Map<TaskDto>(taskItem);

        return Ok(taskDto);
    }

    [HttpPost]
    public ActionResult<TaskDto> CreateTask(TaskDto dto)
    {
        var task = _mapper.Map<Entities.Task>(dto);

        _taskRepo.CreateTask(task);
        _taskRepo.SaveChanges();

        var taskDto = _mapper.Map<TaskDto>(task);

        return CreatedAtAction(nameof(GetTaskById), new { taskDto.Id }, taskDto);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateTask(int id, TaskDto dto)
    {
        if (id != dto.Id)
            return BadRequest();

        var taskEntityFromRepo = _taskRepo.GetTaskById(id);

        if (taskEntityFromRepo == null)
            return NotFound();

        // Updates our entity from db context with values from our incomming dto
        _mapper.Map(dto, taskEntityFromRepo);
        _taskRepo.SaveChanges();

        return NoContent();
    }


    [HttpDelete("{id}")]
    public ActionResult DeleteTask(int id)
    {
        var taskEntityFromRepo = _taskRepo.GetTaskById(id);

        if (taskEntityFromRepo == null)
            return NotFound();

        _taskRepo.DeleteTask(taskEntityFromRepo);
        _taskRepo.SaveChanges();

        return NoContent();
    }
}