namespace TaskOrganizer.Shared.Factories
{
    public class TaskBoardFactory : ITaskBoardFactory
    {
        /// <inheritdoc/>
        public TaskBoardDto Create(string name)
        {
            return new TaskBoardDto()
            {
                Name = name
            };
        }

        /// <inheritdoc/>
        public TaskBoardDto Create()
        {
            return new TaskBoardDto();
        }
    }
}
