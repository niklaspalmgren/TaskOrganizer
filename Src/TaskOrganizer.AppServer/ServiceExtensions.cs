namespace TaskOrganizer.AppServer;

public static class ServiceExtensions
{
    public static IServiceCollection AddHttpClients(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        // Http client configurations
        services.AddHttpClient("default", c =>
        {
            c.BaseAddress = new Uri(configurationManager.GetValue<string>("ApiBaseUri"));
        });

        services.AddHttpClient("Tasks", c =>
        {
            c.BaseAddress = new Uri(configurationManager.GetValue<string>("TasksApiBaseUri"));
        });

        services.AddHttpClient("Users", c =>
        {
            c.BaseAddress = new Uri(configurationManager.GetValue<string>("UsersApiBaseUri"));
        });

        services.AddHttpClient("TaskBoards", c =>
        {
            c.BaseAddress = new Uri(configurationManager.GetValue<string>("TaskBoardsApiBaseUri"));
        });

        return services;
    }
}