using TaskOrganizer.Api.Data;

namespace TaskOrganizer.Api.Services
{
    public class TaskRepo : ITaskRepo
    {
        private readonly TasksDb _context;

        public TaskRepo(TasksDb context)
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
