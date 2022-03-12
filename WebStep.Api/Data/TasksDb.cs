using Microsoft.EntityFrameworkCore;

namespace WebStep.Api.Data
{
    public class TasksDb : DbContext
    {
        public TasksDb(DbContextOptions<TasksDb> options) : base(options)
        {
        }

        public DbSet<Entities.Task> Tasks { get; set; }
        public DbSet<Entities.TaskBoard> TaskBoards { get; set; }
    }
}
