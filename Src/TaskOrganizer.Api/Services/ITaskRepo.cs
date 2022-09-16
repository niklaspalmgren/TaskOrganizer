namespace TaskOrganizer.Api.Services
{
    public interface ITaskRepo
    {
        public Task<List<Entities.Task>> GetTasksAsync(int? taskBoardId, string filter);
        public Task<Entities.Task?> GetTaskByIdAsync(int id);
        public void AddTask(Entities.Task task);
        public void DeleteTask(Entities.Task task);
        public Task SaveChangesAsync();
    }
}
