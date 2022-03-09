using Api.Dtos;
using Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

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
    public async Task<ActionResult<TaskDto>> GetTaskByIdAsync(int id)
    {
        var taskItem = await _taskRepo.GetTaskByIdAsync(id);

        if (taskItem is null)
            return NotFound();

        var taskDto = _mapper.Map<TaskDto>(taskItem);

        return Ok(taskDto);
    }
}