namespace TaskOrganizer.AppServer.DragDrop
{
    public interface IDragDropHandler
    {
        Func<object, Task> DropTask { get; }
        object? PayLoad { get; set; }
        string Zone { get; }
        Task HandleDropAsync();
    }

    public class DragHandler : IDragDropHandler
    {
        public DragHandler(Func<object, Task> dropTask, object payLoad, string zone)
        {
            DropTask = dropTask;
            PayLoad = payLoad;
            Zone = zone;
        }

        public Func<object, Task> DropTask { get;  }
        public object? PayLoad { get; set; }
        public string Zone { get; }

        public async Task HandleDropAsync()
        {
            await DropTask(PayLoad);
        }
    }

    public class DropHandler : IDragDropHandler
    {
        public DropHandler(Func<object, Task> dropTask, string zone)
        {
            DropTask = dropTask;
            Zone = zone;
        }

        public Func<object, Task> DropTask { get; }
        public string Zone { get; }
        public object? PayLoad { get; set; }

        public async Task HandleDropAsync()
        {
            await DropTask(PayLoad);
        }
    }
}
