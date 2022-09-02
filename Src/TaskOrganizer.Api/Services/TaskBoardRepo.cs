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

        public IEnumerable<TaskBoard> GetAllTaskBoards()
        {
            var tasks = _context.TaskBoards.Include(x => x.Tasks).ToList();
            return tasks;
        }

        public TaskBoard GetTaskBoardById(int id)
        {
            var task = _context.TaskBoards.Find(id);
            return task;
        }

        public void CreateTaskBoard(TaskBoard taskBoard)
        {
            if (taskBoard == null)
                throw new ArgumentNullException(nameof(taskBoard));

            _context.TaskBoards.Add(taskBoard);
        }

        public void DeleteTaskBoardAndRelatedTasks(TaskBoard taskBoard)
        {
            if (taskBoard == null)
                throw new ArgumentNullException(nameof(taskBoard));

            var taskBoardWithTasks = _context.TaskBoards.Include(x => x.Tasks).First(x => x.Id == taskBoard.Id);

            // Will cascade delete the related tasks when context is saved.
            _context.Remove(taskBoardWithTasks);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }


    }
}
