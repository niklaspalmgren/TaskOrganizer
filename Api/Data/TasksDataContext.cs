using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class TasksDataContext : DbContext
    {
        public TasksDataContext(DbContextOptions<TasksDataContext> options) : base(options)
        {
        }

        public DbSet<Entities.Task> Tasks { get; set; }
        public DbSet<Entities.TaskBoard> TaskBoards { get; set; }
    }
}
