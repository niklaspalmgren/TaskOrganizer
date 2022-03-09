namespace Api.Services
{
    public interface ITaskRepo
    {
        public IEnumerable<Entities.Task> GetAllTasks();
        public Task<Entities.Task> GetTaskByIdAsync(int id);
    }
}
