using Microsoft.EntityFrameworkCore;
using TaskOrganizer.Api.Data;
using TaskOrganizer.Api.Entities;

namespace TaskOrganizer.Api.Services
{
    public class TaskBoardRepo : ITaskBoardRepo
    {
        private readonly TasksDb _context;

        public TaskBoardRepo(TasksDb context)
        {
            _context = context;
        }

        public async Task<List<TaskBoard>> GetTaskBoardsAsync()
        {
            var taskBoards = await _context.TaskBoards.ToListAsync();
            return taskBoards;
        }

        private static IQueryable<TaskBoard> AddFilterToQueryable(string filter, IQueryable<TaskBoard> queryable)
        {

            if (!string.IsNullOrWhiteSpace(filter))
            {
                queryable = queryable
                    .Include(x => x.Tasks
                        .Where(y => y.Name.Contains(filter) || y.Description.Contains(filter)));
            }
            else
            {
                queryable = queryable.Include(x => x.Tasks);
            }

            return queryable;
        }

        public async Task<TaskBoard?> GetTaskBoardByIdAsync(int id)
        {
            var taskBoard = await _context.TaskBoards.FindAsync(id);
            return taskBoard;
        }

        public void AddTaskBoard(TaskBoard taskBoard)
        {
            if (taskBoard == null)
                throw new ArgumentNullException(nameof(taskBoard));

            _context.TaskBoards.Add(taskBoard);
        }

        public async System.Threading.Tasks.Task DeleteTaskBoardAndRelatedTasksAsync(TaskBoard taskBoard)
        {
            if (taskBoard == null)
                throw new ArgumentNullException(nameof(taskBoard));

            var taskBoardWithTasks = await _context.TaskBoards.Include(x => x.Tasks).FirstAsync(x => x.Id == taskBoard.Id);

            // Will cascade delete the related tasks when context is saved.
            _context.Remove(taskBoardWithTasks);
        }

        public async System.Threading.Tasks.Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}
