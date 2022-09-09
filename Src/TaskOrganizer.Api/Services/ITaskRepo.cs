namespace TaskOrganizer.Api.Services
{
    public interface ITaskRepo
    {
        public Task<List<Entities.Task>> GetTasksAsync();
        public Task<Entities.Task?> GetTaskByIdAsync(int id);
        public void CreateTask(Entities.Task task);
        public void DeleteTask(Entities.Task task);
        public Task SaveChangesAsync();
    }
}
