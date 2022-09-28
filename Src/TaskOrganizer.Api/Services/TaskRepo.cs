using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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

        public async Task<List<Entities.Task>> GetTasksAsync(int? taskBoardId, string? searchString)
        {
            var queryable = _context.Tasks.AsQueryable();
            queryable = AddFilterToQueryable(queryable, taskBoardId, searchString);

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

        private IQueryable<Entities.Task> AddFilterToQueryable(IQueryable<Entities.Task> queryable, int? taskBoardId, string? searchString)
        {
            // Filter by task board
            if (taskBoardId is int id)
            {
                queryable = queryable.Where(x => x.TaskBoardId == id);
            }

            // Filter by wild card match
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                queryable = queryable.Join(_context.Users, x => x.UserId, u => u.Id, (task, user) => new
                {
                    CombinedSearchString = task.Name + task.Description + user.FirstName + user.LastName,
                    Task = task
                }).Where(x => x.CombinedSearchString.Contains(searchString)).Select(x => x.Task);
            }

            queryable = queryable.OrderBy(x => x.Order);

            return queryable;
        }


    }
}
