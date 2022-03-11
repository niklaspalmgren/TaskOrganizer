using WebStep.Dto;

namespace webStep.AppServer.Data
{
    public class TaskService : ITaskService
    {
        private readonly IHttpClientFactory _clientFactory;

        public TaskService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<List<TaskDto>> GetAllTasksAsync()
        {
            var httpClient = _clientFactory.CreateClient("Tasks");
            var tasks = await httpClient.GetFromJsonAsync<List<TaskDto>>(string.Empty);

            return tasks ?? new List<TaskDto>();
        }

        public async Task<TaskDto> CreateTaskAsync(TaskDto taskDto)
        {
            var httpClient = _clientFactory.CreateClient("Tasks");
            var response = await httpClient.PostAsJsonAsync(string.Empty, taskDto);

            var createdTask = await response.Content.ReadFromJsonAsync<TaskDto>();

            return createdTask;
        }
    }
}
