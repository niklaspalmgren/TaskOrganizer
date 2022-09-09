using TaskOrganizer.Shared;

namespace TaskOrganizer.AppServer.Services
{
    public class TaskService : ITaskService
    {
        private readonly IHttpClientFactory _clientFactory;

        public TaskService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<TaskDto> GetTask(int id)
        {
            var httpClient = _clientFactory.CreateClient("Tasks");
            var uri = $"{id}";
            var task = await httpClient.GetFromJsonAsync<TaskDto>(uri);

            return task;
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

        public async Task UpdateTaskAsync(TaskDto taskDto)
        {
            var httpClient = _clientFactory.CreateClient("Tasks");
            var uri = $"{taskDto.Id}";

            await httpClient.PutAsJsonAsync(uri, taskDto);
        }

        public async Task DeleteTaskAsync(TaskDto taskDto)
        {
            var httpClient = _clientFactory.CreateClient("Tasks");
            var uri = $"{taskDto.Id}";

            await httpClient.DeleteAsync(uri);
        }

        
    }
}
