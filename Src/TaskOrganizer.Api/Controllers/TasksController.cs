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
    public async Task<ActionResult<List<TaskDto>>> GetAllTasks()
    {
        var tasksFromRepo = await _taskRepo.GetAllTasksAsync();

        if (tasksFromRepo == null || !tasksFromRepo.Any())
            return NotFound();

        var taskDtos = _mapper.Map<List<Entities.Task>, List<TaskDto>>(tasksFromRepo);

        return Ok(taskDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskDto>> GetTaskById(int id)
    {
        var taskFromRepo = await _taskRepo.GetTaskByIdAsync(id);

        if (taskFromRepo == null)
            return NotFound();

        var taskDto = _mapper.Map<TaskDto>(taskFromRepo);

        return Ok(taskDto);
    }

    [HttpPost]
    public async Task<ActionResult<TaskDto>> CreateTask(TaskDto dto)
    {
        var task = _mapper.Map<Entities.Task>(dto);

        _taskRepo.CreateTask(task);
        await _taskRepo.SaveChangesAsync();

        var taskDto = _mapper.Map<TaskDto>(task);

        return CreatedAtAction(nameof(GetTaskById), new { taskDto.Id }, taskDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTask(int id, TaskDto dto)
    {
        if (id != dto.Id)
            return BadRequest();

        var taskFromRepo = await _taskRepo.GetTaskByIdAsync(id);

        if (taskFromRepo == null)
            return NotFound();

        // Updates our entity from db context with values from our incomming dto
        _mapper.Map(dto, taskFromRepo);
        await _taskRepo.SaveChangesAsync();

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTask(int id)
    {
        var taskFromRepo = await _taskRepo.GetTaskByIdAsync(id);

        if (taskFromRepo == null)
            return NotFound();

        _taskRepo.DeleteTask(taskFromRepo);
        await _taskRepo.SaveChangesAsync();

        return NoContent();
    }
}