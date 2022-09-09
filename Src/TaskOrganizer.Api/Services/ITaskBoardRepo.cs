using TaskOrganizer.Api.Entities;
using Task = System.Threading.Tasks.Task;

namespace TaskOrganizer.Api.Services
{
    public interface ITaskBoardRepo
    {
        public Task<List<TaskBoard>> GetTaskBoardsAsync(string filter);
        public Task<TaskBoard?> GetTaskBoardByIdAsync(int id);
        public void CreateTaskBoard(TaskBoard taskBoard);
        public Task DeleteTaskBoardAndRelatedTasksAsync(TaskBoard taskBoard);
        public Task SaveChangesAsync();

    }
}
