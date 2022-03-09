using Api.Data;

namespace Api.Services
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

        public async Task<Entities.Task> GetTaskByIdAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            return task;
        }
    }
}
