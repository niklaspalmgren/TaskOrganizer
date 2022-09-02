using Microsoft.EntityFrameworkCore;
using TaskOrganizer.Api.Entities;

namespace TaskOrganizer.Api.Data
{
    public class TasksDb : DbContext
    {
        public TasksDb(DbContextOptions<TasksDb> options) : base(options)
        {
        }

        public DbSet<Entities.Task> Tasks { get; set; }
        public DbSet<TaskBoard> TaskBoards { get; set; }
    }
}
