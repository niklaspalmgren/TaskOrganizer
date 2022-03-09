namespace Api.Services
{
    public interface ITaskBoardRepo
    {
        public IEnumerable<Entities.TaskBoard> GetAllTaskBoards();
        public Task<Entities.TaskBoard> GetTaskBoardByIdAsync(int id);
    }
}
