namespace WebStep.Api.Services
{
    public interface ITaskBoardRepo
    {
        public Task<List<Entities.TaskBoard>> GetAllTaskBoardsAsync();
        public Task<Entities.TaskBoard> GetTaskBoardByIdAsync(int id);
    }
}
