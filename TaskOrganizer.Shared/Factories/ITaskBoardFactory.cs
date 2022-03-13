namespace TaskOrganizer.Shared.Factories
{
    public interface ITaskBoardFactory
    {
        /// <summary>
        /// Creates a new task board.
        /// </summary>
        /// <param name="name">The name of the board.</param>
        /// <returns><see cref="TaskBoardDto"/></returns>
        public TaskBoardDto Create(string name);

        /// <summary>
        /// Creates a new empty task board.
        /// </summary>
        /// <returns><see cref="TaskBoardDto"/></returns>
        public TaskBoardDto Create();
    }
}
