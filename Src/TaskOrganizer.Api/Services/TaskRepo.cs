using Microsoft.EntityFrameworkCore;
using TaskOrganizer.Api.Data;
using TaskOrganizer.Api.Entities;

namespace TaskOrganizer.Api.Services
{
    public class TaskRepo : ITaskRepo
    {
        private readonly TasksDb _context;

        public TaskRepo(TasksDb context)
        {
            _context = context;
        }

        public async Task<List<Entities.Task>> GetTasksAsync(int? taskBoardId, string filter)
        {
            var queryable = _context.Tasks.AsQueryable();
            queryable = AddFilterToQueryable(queryable, taskBoardId, filter);

            var tasks = await queryable.ToListAsync();
            return tasks;
        }

        public async Task<Entities.Task?> GetTaskByIdAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            return task;
        }

        public void AddTask(Entities.Task task)
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

        public async System.Threading.Tasks.Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        private IQueryable<Entities.Task> AddFilterToQueryable(IQueryable<Entities.Task> queryable, int? taskBoardId, string filter)
        {
            // Filter by task board
            if (taskBoardId is int id)
            {
                queryable = queryable.Where(x => x.TaskBoardId == id);
            }

            // Filter by wild card match
            if (!string.IsNullOrWhiteSpace(filter))
            {
                queryable = queryable.Where(x => x.Name.Contains(filter) || x.Description.Contains(filter));
            }

            return queryable;
        }
    }
}
