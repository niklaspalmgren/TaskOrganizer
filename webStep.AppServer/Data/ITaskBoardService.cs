using WebStep.Dto;

namespace webStep.AppServer.Data
{
    public interface ITaskBoardService
    {
        Task<List<TaskBoardDto>> GetAllTaskBoardsAsync();
        Task<TaskBoardDto> CreateTaskBoardAsync(TaskBoardDto taskDto);
        Task UpdateTaskBoardAsync(TaskBoardDto taskDto);
        Task DeleteTaskBoardAsync(TaskBoardDto taskDto);
    }
}