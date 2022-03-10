using WebStep.Dto;

namespace webStep.AppServer.Data
{
    public class TaskService : ITaskService
    {
        private readonly HttpClient _client;

        public TaskService(HttpClient httpClient)
        {
            _client = httpClient;
        }

        public async Task<List<TaskDto>> GetAllTasksAsync()
        {
            var uri = "api/tasks";
            var tasks = await _client.GetFromJsonAsync<List<TaskDto>>(uri);

            return tasks ?? new List<TaskDto>();
        }
    }
}
