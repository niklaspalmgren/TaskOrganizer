namespace WebStep.Api.Services
{
    public interface ITaskRepo
    {
        public IEnumerable<Entities.Task> GetAllTasks();
        public Entities.Task GetTaskById(int id);
        public void CreateTask(Entities.Task task);
        public void UpdateTask(Entities.Task task);
        public void DeleteTask(Entities.Task task);
        public void SaveChanges();
    }
}
