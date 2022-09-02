using Microsoft.EntityFrameworkCore;
using TaskOrganizer.Api.Entities;

namespace TaskOrganizer.Api.Data
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

                if (taskBoards != null && taskBoards.Any())
                {
                    foreach (var taskBoard in taskBoards)
                    {
                        switch (taskBoard.Name)
                        {
                            case "Backlog":
                                SeedBacklogTasks(taskBoard);
                                break;
                            case "Ideas":
                                SeedIdeasTasks(taskBoard);
                                break;
                            default:
                                continue;

                        }
                    }
                    context.SaveChanges();
                }


            }
        }

        private static void SeedIdeasTasks(TaskBoard taskBoard)
        {
            var tasks = new List<Entities.Task>()
                    {
                        new Entities.Task()
                        {
                            Name = "Drag drop",
                            Description = "Move tasks between task boards using drag and drop operations.",
                            TaskBoardId = taskBoard.Id,
                        },
                        new Entities.Task()
                        {
                            Name = "User confirmation dialogs",
                            Description = "Confirmation dialogs to confirm deletion of tasks- and task boards.",
                            TaskBoardId = taskBoard.Id
                        },
                    };

            taskBoard.Tasks = tasks;
        }

        private static void SeedBacklogTasks(TaskBoard taskBoard)
        {
            var tasks = new List<Entities.Task>()
                    {
                        new Entities.Task()
                        {
                            Name = "Docker HTTPS Support",
                            Description = "Add support for https when the apps are run in docker containers.",
                            TaskBoardId = taskBoard.Id,
                        },
                        new Entities.Task()
                        {
                            Name = "Patching",
                            Description = "Update the controllers to support patching. PUT is now the only option to update entities.",
                            TaskBoardId = taskBoard.Id
                        },

                        new Entities.Task()
                        {
                            Name = "Improve error handling",
                            Description = "Add exception filters that can be used by the exception handler to give a better response to the client.",
                            TaskBoardId = taskBoard.Id
                        }
                    };

            taskBoard.Tasks = tasks;
        }
    }
}
