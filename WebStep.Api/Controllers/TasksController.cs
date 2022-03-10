using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebStep.Api.Services;
using WebStep.Dto;

namespace WebStep.Api.Controllers;

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

    [HttpGet("{id}", Name = "GetTaskById")]
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

        return CreatedAtRoute(nameof(GetTaskById), new { Id = taskDto.Id }, taskDto);
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