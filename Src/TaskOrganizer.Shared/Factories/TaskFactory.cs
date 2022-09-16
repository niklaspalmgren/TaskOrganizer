namespace TaskOrganizer.Shared.Factories
{
    public class TaskFactory : ITaskFactory
    {

        /// <inheritdoc/>
        public TaskDto Create(string name, string description, int taskBoardId)
        {
            return new TaskDto()
            {
                Name = name,
                Description = description,
                TaskBoardId = taskBoardId
            };
        }

        /// <inheritdoc/>
        public TaskDto Create(int taskBoardId)
        {
            return new TaskDto()
            {
                TaskBoardId = taskBoardId
            };
        }
    }
}
