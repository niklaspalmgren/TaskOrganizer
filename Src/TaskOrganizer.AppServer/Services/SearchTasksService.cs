namespace TaskOrganizer.AppServer.Services;

public sealed class SearchTasksService : ISearchTasksService
{
    public event Func<string, Task>? Notify;
    public string SearchValue { get; private set; } = string.Empty;
    public bool IsSearching => !string.IsNullOrWhiteSpace(SearchValue);

    public async Task SearchAsync(string value)
    {
        SearchValue = value.Trim();
        
        if (Notify != null)
        {
            await Notify.Invoke(SearchValue);
        }
    }
}