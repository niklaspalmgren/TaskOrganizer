using Polly;
using Polly.Contrib.WaitAndRetry;
using System.Net;

namespace TaskOrganizer.AppServer;

public static class ServiceExtensions
{

    /// <summary>
    /// Adds http client services.
    /// </summary>
    public static IServiceCollection AddHttpClients(this IServiceCollection services, ConfigurationManager configurationManager)
    {        
        services.AddHttpClient("default", c =>
        {
            c.BaseAddress = new Uri(configurationManager.GetValue<string>("ApiBaseUri"));
        }).AddPolicyHandler(HttpRetryPolicies.GetRetryPolicy);

        services.AddHttpClient("Tasks", c =>
        {
            c.BaseAddress = new Uri(configurationManager.GetValue<string>("TasksApiBaseUri"));
        }).AddPolicyHandler(HttpRetryPolicies.GetRetryPolicy);

        services.AddHttpClient("Users", c =>
        {
            c.BaseAddress = new Uri(configurationManager.GetValue<string>("UsersApiBaseUri"));
        }).AddPolicyHandler(HttpRetryPolicies.GetRetryPolicy);

        services.AddHttpClient("TaskBoards", c =>
        {
            c.BaseAddress = new Uri(configurationManager.GetValue<string>("TaskBoardsApiBaseUri"));
        }).AddPolicyHandler(HttpRetryPolicies.GetRetryPolicy);

        return services;
    }
}