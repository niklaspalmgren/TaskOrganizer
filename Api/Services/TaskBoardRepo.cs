using Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class TaskBoardRepo : ITaskBoardRepo
    {
        private readonly TasksDataContext _context;

        public TaskBoardRepo(TasksDataContext context)
        {
            _context = context;
        }

        public IEnumerable<Entities.TaskBoard> GetAllTaskBoards()
        {
            var tasks = _context.TaskBoards.Include(x => x.Tasks);
            return tasks;
        }

        public async Task<Entities.TaskBoard> GetTaskBoardByIdAsync(int id)
        {
            var task = await _context.TaskBoards.FindAsync(id);
            return task;
        }
    }
}
