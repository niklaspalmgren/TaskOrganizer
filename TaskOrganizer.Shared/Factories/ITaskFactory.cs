namespace TaskOrganizer.Shared.Factories
{
    public interface ITaskFactory
    {
        /// <summary>
        /// Creates a new task.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="description">The task description.</param>
        /// <param name="boardId">The ID of the <see cref="TaskBoardDto"/> it belongs to.</param>
        /// <returns><see cref="TaskBoardDto"/></returns>
        public TaskDto Create(string name, string description, int boardId);

        /// <summary>
        /// Creates a new empty task.
        /// </summary>
        /// <param name="boardId">The ID of the <see cref="TaskBoardDto"/> it belongs to.</param>
        /// <returns><see cref="TaskDto"/></returns>
        public TaskDto Create(int boardId);
    }
}
