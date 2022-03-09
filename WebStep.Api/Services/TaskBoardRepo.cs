using Microsoft.EntityFrameworkCore;
using WebStep.Api.Data;
using WebStep.Api.Entities;

namespace WebStep.Api.Services
{
    public class TaskBoardRepo : ITaskBoardRepo
    {
        private readonly TasksDataContext _context;

        public TaskBoardRepo(TasksDataContext context)
        {
            _context = context;
        }

        public async Task<List<TaskBoard>> GetAllTaskBoardsAsync()
        {
            var tasks = await _context.TaskBoards.Include(x => x.Tasks).ToListAsync();
            return tasks;
        }

        public async Task<TaskBoard> GetTaskBoardByIdAsync(int id)
        {
            var task = await _context.TaskBoards.FindAsync(id);
            return task;
        }
    }
}
