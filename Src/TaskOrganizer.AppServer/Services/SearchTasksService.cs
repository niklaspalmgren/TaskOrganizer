namespace TaskOrganizer.AppServer.Services;

public sealed class SearchTasksService : ISearchTasksService
{
    public event Func<SearchTaskEventArgs, Task>? Notify;
    public string Wildcard { get; set; } = string.Empty;
    public int? UserId { get; set; }
    public bool IsSearching { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="wildcard">Search string.</param>
    /// <param name="userId">User id.</param>
    /// <returns></returns>
    public async Task SearchAsync(string wildcard, int? userId = null)
    {
        var handler = Notify;

        if (handler is null)
            return;

        Wildcard = wildcard;

        if (userId is not null)
            UserId = userId;

        var invocationList = handler.GetInvocationList();
        var handlerTasks = new Task[invocationList.Length];

        for (var i = 0; i < invocationList.Length; i++)
        {
            handlerTasks[i] = ((Func<SearchTaskEventArgs, Task>)invocationList[i])(new SearchTaskEventArgs(Wildcard, UserId));
        }

        await Task.WhenAll(handlerTasks);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="wildcard">Search string.</param>
    /// <param name="userId">User id.</param>
    /// <returns></returns>
    public async Task SearchAsync(int? userId)
    {
        var handler = Notify;

        if (handler is null)
            return;

        if (userId is not null)
            UserId = userId;

        var invocationList = handler.GetInvocationList();
        var handlerTasks = new Task[invocationList.Length];

        for (var i = 0; i < invocationList.Length; i++)
        {
            handlerTasks[i] = ((Func<SearchTaskEventArgs, Task>)invocationList[i])(new SearchTaskEventArgs(Wildcard, UserId));
        }

        await Task.WhenAll(handlerTasks);
    }
}