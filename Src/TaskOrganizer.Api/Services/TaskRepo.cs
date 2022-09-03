using Microsoft.EntityFrameworkCore;
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


        public async Task<List<Entities.Task>> GetAllTasksAsync()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return tasks;
        }

        public async Task<Entities.Task?> GetTaskByIdAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
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

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
