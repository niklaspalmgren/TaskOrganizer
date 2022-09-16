namespace TaskOrganizer.AppServer.Services;

public interface ISearchTasksService
{
    public event Func<string, Task>? Notify;
    public string SearchValue { get; }
    public bool IsSearching { get; }
    public Task SearchAsync(string value);
}