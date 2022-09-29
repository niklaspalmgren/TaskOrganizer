using System;

namespace TaskOrganizer.AppServer.Services;

public interface ISearchTasksService
{
    public event Func<SearchTaskEventArgs, Task>? Notify;
    public string Wildcard { get; } 
    public int? UserId { get; }
    public bool IsSearching { get; }
    public Task SearchAsync(string wildcard, int? userId = null);
    public Task SearchAsync(int? userId);
}