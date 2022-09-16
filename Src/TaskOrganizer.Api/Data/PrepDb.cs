using Microsoft.EntityFrameworkCore;
using TaskOrganizer.Api.Entities;
using Task = TaskOrganizer.Api.Entities.Task;

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
            if (!context.Users.Any())
            {
                var users = new List<User>()
                {
                    new()
                    {
                        FirstName = "Niklas",
                        LastName = "Palmgren"
                    },
                    new()
                    {
                        FirstName = "Martin",
                        LastName = "Fast"
                    }
                };

                context.Users.AddRange(users);
                context.SaveChanges();
            }
            
            if (!context.TaskBoards.Any())
            {
                var taskBoards = new List<TaskBoard>()
                    {
                        new()
                        {
                            Name = "Backlog"
                        },
                        new()
                        {
                            Name = "Ideas"
                        }
                    };

                context.TaskBoards.AddRange(taskBoards);
                context.SaveChanges();
            }

            if (!context.Tasks.Any())
            {

                var taskBoards = context.TaskBoards.ToList();
                var users = context.Users.ToList();

                if (!taskBoards.Any()) 
                    return;
                
                foreach (var taskBoard in taskBoards)
                {
                    switch (taskBoard.Name)
                    {
                        case "Backlog":
                            SeedBacklogTasks(taskBoard, users.FirstOrDefault());
                            break;
                        case "Ideas":
                            SeedIdeasTasks(taskBoard, users.FirstOrDefault());
                            break;
                        default:
                            continue;

                    }
                }
                context.SaveChanges();


            }
        }

        private static void SeedIdeasTasks(TaskBoard taskBoard, User user)
        {
            var tasks = new List<Entities.Task>()
                    {
                        new()
                        {
                            Name = "Drag drop",
                            Description = "Move tasks between task boards using drag and drop operations.",
                            TaskBoardId = taskBoard.Id
                        },
                        new()
                        {
                            Name = "User confirmation dialogs",
                            Description = "Confirmation dialogs to confirm deletion of tasks- and task boards.",
                            TaskBoardId = taskBoard.Id
                        },
                    };

            taskBoard.Tasks = tasks;
        }

        private static void SeedBacklogTasks(TaskBoard taskBoard, User user)
        {
            var tasks = new List<Entities.Task>()
                    {
                        new()
                        {
                            Name = "Docker HTTPS Support",
                            Description = "Add support for https when the apps are run in docker containers.",
                            TaskBoardId = taskBoard.Id
                        },
                        new()
                        {
                            Name = "Patching",
                            Description = "Update the controllers to support patching. PUT is now the only option to update entities.",
                            TaskBoardId = taskBoard.Id
                        },

                        new()
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
