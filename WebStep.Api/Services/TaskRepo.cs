using WebStep.Api.Data;

namespace WebStep.Api.Services
{
    public class TaskRepo : ITaskRepo
    {
        private readonly TasksDataContext _context;

        public TaskRepo(TasksDataContext context)
        {
            _context = context;
        }

        
        public IEnumerable<Entities.Task> GetAllTasks()
        {
            var tasks = _context.Tasks;
            return tasks;
        }

        public Entities.Task GetTaskById(int id)
        {
            var task = _context.Tasks.Find(id);
            return task;
        }

        public void CreateTask(Entities.Task task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            _context.Tasks.Add(task);
        }

        public void DeleteTask(Entities.Task task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            _context.Tasks.Remove(task);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
