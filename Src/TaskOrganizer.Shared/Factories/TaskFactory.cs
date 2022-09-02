namespace TaskOrganizer.Shared.Factories
{
    public class TaskFactory : ITaskFactory
    {

        /// <inheritdoc/>
        public TaskDto Create(string name, string description, int boardId)
        {
            return new TaskDto()
            {
                Name = name,
                Description = description,
                TaskBoardId = boardId
            };
        }

        /// <inheritdoc/>
        public TaskDto Create(int boardId)
        {
            return new TaskDto()
            {
                TaskBoardId = boardId
            };
        }
    }
}
