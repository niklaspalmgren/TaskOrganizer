using Microsoft.EntityFrameworkCore;
using WebStep.Api.Entities;

namespace WebStep.Api.Data
{
    public static class PrepDb
    {
        public static void Prep(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var context = serviceScope.ServiceProvider.GetService<TasksDb>();

            if (context == null)
                return;

            Migrate(context);
            SeedData(context);
        }

        public static void Migrate(TasksDb context)
        {
            context.Database.Migrate();
        }

        public static void SeedData(TasksDb context)
        {
            if (!context.TaskBoards.Any())
            {
                var taskBoards = new List<TaskBoard>()
                    {
                        new TaskBoard()
                        {
                            Name = "Backlog"
                        },
                        new TaskBoard()
                        {
                            Name = "Ideas"
                        }
                    };

                context.TaskBoards.AddRange(taskBoards);
                context.SaveChanges();
            }

            if (!context.Tasks.Any())
            {

                var taskBoards = context.TaskBoards;

                if (taskBoards != null && taskBoards.Any)
                {
                    var tasks = new List<Entities.Task>()
                    {
                        new Entities.Task()
                        {
                            Name = "Task 1",
                            Description = "The first task.",
                            TaskBoardId = taskBoard.Id,
                        },
                        new Entities.Task()
                        {
                            Name = "Task 2",
                            Description = "The second task.",
                            TaskBoardId = taskBoard.Id
                        },
                    };

                    context.Tasks.AddRange(tasks);
                    context.SaveChanges();
                }


            }



        }
    }
}
