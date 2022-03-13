using TaskOrganizer.Api.Entities;

namespace TaskOrganizer.Api.Services
{
    public interface ITaskBoardRepo
    {
        public IEnumerable<TaskBoard> GetAllTaskBoards();
        public TaskBoard GetTaskBoardById(int id);
        public void CreateTaskBoard(TaskBoard taskBoard);
        public void DeleteTaskBoardAndRelatedTasks(TaskBoard taskBoard);
        public void SaveChanges();

    }
}
