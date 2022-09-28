using TaskOrganizer.Shared;

namespace TaskOrganizer.AppServer
{
    public interface ITaskReorderer<T> where T : IList<TaskDto>
    {
        public T OrderByNameAsc(T tasks);
        public T OrderByNameDesc(T tasks);
    }

    public class TaskReorderer : ITaskReorderer<List<TaskDto>>
    {
        public List<TaskDto> OrderByNameAsc(List<TaskDto> tasks)
        {
            var orderedTasks =  tasks.OrderBy(x => x.Name).ToList();

            UpdateOrderProperty(orderedTasks);

            return orderedTasks;

        }

        public List<TaskDto> OrderByNameDesc(List<TaskDto> tasks)
        {
            var orderedTasks = tasks.OrderByDescending(x => x.Name).ToList();

            UpdateOrderProperty(orderedTasks);

            return orderedTasks;
        }

        private void UpdateOrderProperty(List<TaskDto> tasks)
        {
            for (var i = 0; i < tasks.Count; i++)
            {
                tasks[i].Order = i;
            }
        }
    }

    
}
