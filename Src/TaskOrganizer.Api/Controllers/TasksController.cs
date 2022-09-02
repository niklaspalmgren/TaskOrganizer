using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskOrganizer.Api.Services;
using TaskOrganizer.Shared;

namespace TaskOrganizer.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ITaskRepo _taskRepo;

    public TasksController(
        IMapper mapper,
        ITaskRepo taskRepo)
    {
        _mapper = mapper;
        _taskRepo = taskRepo;
    }


    [HttpGet]
    public ActionResult<IEnumerable<TaskDto>> GetAllTasks()
    {
        var tasksFromRepo = _taskRepo.GetAllTasks();

        if (tasksFromRepo == null || !tasksFromRepo.Any())
            return NotFound();

        var taskDtos = _mapper.Map<IEnumerable<Entities.Task>, IEnumerable<TaskDto>>(tasksFromRepo);

        return Ok(taskDtos);
    }

    [HttpGet("{id}")]
    public ActionResult<TaskDto> GetTaskById(int id)
    {
        var taskFromRepo = _taskRepo.GetTaskById(id);

        if (taskFromRepo == null)
            return NotFound();

        var taskDto = _mapper.Map<TaskDto>(taskFromRepo);

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

        var taskFromRepo = _taskRepo.GetTaskById(id);

        if (taskFromRepo == null)
            return NotFound();

        // Updates our entity from db context with values from our incomming dto
        _mapper.Map(dto, taskFromRepo);
        _taskRepo.SaveChanges();

        return NoContent();
    }


    [HttpDelete("{id}")]
    public ActionResult DeleteTask(int id)
    {
        var taskFromRepo = _taskRepo.GetTaskById(id);

        if (taskFromRepo == null)
            return NotFound();

        _taskRepo.DeleteTask(taskFromRepo);
        _taskRepo.SaveChanges();

        return NoContent();
    }
}