using TaskOrganizer.AppServer.Services;
using TaskOrganizer.Shared;

namespace TaskOrganizer.AppServer
{
    public interface ITaskManager
    {
        List<TaskBoardDto> TaskBoards { get; }

        bool IsUsingFilter { get; }
        string FilterString { get; set; }

        event Action? OnChange;

        // Tasks
        TaskBoardDto? GetTaskBoardById(int id);
        Task RemoveTaskAsync(TaskDto taskDto);
        Task UpdateTaskAsync(TaskDto task);
        Task AddTaskAsync(TaskDto taskDto);
        
        // Task boards
        Task LoadTaskBoardsAsync(string filter);
        Task UpdateTaskBoardAsync(TaskBoardDto taskBoard);
        Task RemoveTaskBoardAsync(TaskBoardDto taskBoard);
        Task AddTaskBoardAsync(TaskBoardDto taskBoardDto);
    }

    public class TaskManager : ITaskManager
    {
        private readonly ITaskBoardService _taskBoardService;
        private readonly ITaskService _taskService;

        public bool IsUsingFilter => !string.IsNullOrWhiteSpace(FilterString);

        public string FilterString { get; set; } = string.Empty;

        public TaskManager(ITaskBoardService taskBoardService, ITaskService taskService)
        {
            _taskBoardService = taskBoardService;
            _taskService = taskService;
        }

        public List<TaskBoardDto> TaskBoards { get; private set; } = Enumerable.Empty<TaskBoardDto>().ToList();

        public TaskBoardDto? GetTaskBoardById(int id)
        {
            return TaskBoards.FirstOrDefault(x => x.Id == id);
        }

        public async Task LoadTaskBoardsAsync(string filter)
        {
            TaskBoards = await _taskBoardService.GetTaskBoardsAsync(filter);
        }

        /// <summary>
        /// Removes the given taskboard with its tasks.
        /// </summary>
        /// <param name="taskBoard"></param>
        public async Task RemoveTaskBoardAsync(TaskBoardDto taskBoard)
        {
            var toDelete = TaskBoards.FirstOrDefault(x => x.Id == taskBoard.Id);

            if (toDelete is null)
                return;

            await _taskBoardService.DeleteTaskBoardAsync(toDelete);
            TaskBoards.Remove(taskBoard);
            
            NotifyStateChanged();
        }

        /// <summary>
        /// Remove given task.
        /// </summary>
        /// <param name="taskDto"></param>
        public async Task RemoveTaskAsync(TaskDto taskDto)
        {
            var taskBoard = TaskBoards.Where(x => x.Tasks.Contains(taskDto)).FirstOrDefault();

            if (taskBoard is null)
                return;

            await _taskService.DeleteTaskAsync(taskDto);
            taskBoard.Tasks.Remove(taskDto);
            
            NotifyStateChanged();
        }

        public async Task AddTaskBoardAsync(TaskBoardDto taskBoardDto)
        {
            var createdTaskBoard = await _taskBoardService.CreateTaskBoardAsync(taskBoardDto);

            TaskBoards.Add(createdTaskBoard);
            
            NotifyStateChanged();
        }
        
        public async Task AddTaskAsync(TaskDto taskDto)
        {
            var createdTaskBoard = await _taskService.CreateTaskAsync(taskDto);

            TaskBoards.FirstOrDefault(x => x.Id == createdTaskBoard.TaskBoardId)?.Tasks.Add(createdTaskBoard);
            
            NotifyStateChanged();
        }

        public async Task UpdateTaskAsync(TaskDto task)
        {
            await _taskService.UpdateTaskAsync(task);

            // Remove from old taskboard
            var oldTaskBoard = TaskBoards.FirstOrDefault(x => x.Tasks.Contains(task));
            oldTaskBoard?.Tasks.Remove(task);

            // Add to new
            var newTaskBoard = TaskBoards.FirstOrDefault(x => x.Id == task.TaskBoardId);
            newTaskBoard?.Tasks.Add(task);

            NotifyStateChanged();
        }

        public async Task UpdateTaskBoardAsync(TaskBoardDto taskBoard)
        {
            await _taskBoardService.UpdateTaskBoardAsync(taskBoard);
        }


        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}