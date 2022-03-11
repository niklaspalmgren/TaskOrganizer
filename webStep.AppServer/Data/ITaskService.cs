using WebStep.Dto;

namespace webStep.AppServer.Data
{
    public interface ITaskService
    {
        Task<List<TaskDto>> GetAllTasksAsync();
        Task<TaskDto> CreateTaskAsync(TaskDto taskDto);
        Task UpdateTaskAsync(TaskDto taskDto);
        Task DeleteTaskAsync(TaskDto taskDto);
    }
}