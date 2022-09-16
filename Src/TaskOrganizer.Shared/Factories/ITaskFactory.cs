namespace TaskOrganizer.Shared.Factories
{
    public interface ITaskFactory
    {
        /// <summary>
        /// Creates a new task.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="description">The task description.</param>
        /// <param name="taskBoardId">The task board id.</param>
        /// <returns><see cref="TaskDto"/></returns>
        public TaskDto Create(string name, string description, int taskBoardId);

        /// <summary>
        /// Creates a new empty task.
        /// </summary>
        /// <param name="taskBoardId">The task board id.</param>
        /// <returns><see cref="TaskDto"/></returns>
        public TaskDto Create(int taskBoard);
    }
}
