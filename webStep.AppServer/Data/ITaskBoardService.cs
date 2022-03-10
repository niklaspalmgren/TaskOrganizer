using WebStep.Dto;

namespace webStep.AppServer.Data
{
    public interface ITaskBoardService
    {
        Task<List<TaskBoardDto>> GetAllTaskBoardsAsync();
    }
}