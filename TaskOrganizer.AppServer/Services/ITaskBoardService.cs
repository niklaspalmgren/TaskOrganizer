using TaskOrganizer.Shared;

namespace TaskOrganizer.AppServer.Services
{
    public interface ITaskBoardService
    {
        Task<List<TaskBoardDto>> GetAllTaskBoardsAsync();
        Task<TaskBoardDto> CreateTaskBoardAsync(TaskBoardDto taskDto);
        Task UpdateTaskBoardAsync(TaskBoardDto taskDto);
        Task DeleteTaskBoardAsync(TaskBoardDto taskDto);
    }
}