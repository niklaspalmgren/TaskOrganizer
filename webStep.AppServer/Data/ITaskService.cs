using WebStep.Dto;

namespace webStep.AppServer.Data
{
    public interface ITaskService
    {
        Task<List<TaskDto>> GetAllTasksAsync();
        Task<TaskDto> CreateTaskAsync(TaskDto taskDto);
    }
}