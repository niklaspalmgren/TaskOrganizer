namespace TaskOrganizer.AppServer.Services;


public interface INotifierService
{
    
}
public class NotifierService : INotifierService
{
    public async Task UpdateAsync(string key, int value)
    {
        if (Notify != null)
        {
            await Notify.Invoke(key, value);
        }
    }

    public event Func<string, int, Task>? Notify;
}