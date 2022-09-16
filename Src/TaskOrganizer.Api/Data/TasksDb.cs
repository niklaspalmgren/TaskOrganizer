using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TaskOrganizer.Api.Entities;

namespace TaskOrganizer.Api.Data
{
    public class TasksDb : DbContext
    {
        public TasksDb(DbContextOptions<TasksDb> options) : base(options)
        {
        }

        public DbSet<Entities.Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TaskBoard> TaskBoards { get; set; }
    }
}
