using System.Net;
using TaskOrganizer.Shared;

namespace TaskOrganizer.AppServer.Services
{
    public class TaskService : ITaskService
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string _httpClientName = "Tasks";

        public TaskService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<TaskDto> GetTask(int id)
        {
            var httpClient = _clientFactory.CreateClient(_httpClientName);
            var uri = $"{id}";
            var task = await httpClient.GetFromJsonAsync<TaskDto>(uri);

            return task;
        }

        public async Task<List<TaskDto>> GetTasksAsync()
        {
            var httpClient = _clientFactory.CreateClient(_httpClientName);
            var tasks = await httpClient.GetFromJsonAsync<List<TaskDto>>(string.Empty);

            return tasks ?? new List<TaskDto>();
        }

        public async Task<List<TaskDto>> GetTasksForBoardAsync(int taskBoardId, int? UserId = null, string filter = "")
        {
            var httpClient = _clientFactory.CreateClient(_httpClientName);

            string uri;

            if (string.IsNullOrWhiteSpace(filter))
            {
                uri = $"?taskBoardId={taskBoardId}";
            }
            else
            {
                uri = $"?taskBoardId={taskBoardId}&filter={filter}";
            }

            List<TaskDto> tasks = new();

            try
            {
                tasks = await httpClient.GetFromJsonAsync<List<TaskDto>>(uri);
            }
            catch(HttpRequestException ex)
            {
                if (ex.StatusCode != HttpStatusCode.NotFound)
                {
                    throw;
                }
            }

            return tasks ?? new();
        }

        public async Task<TaskDto> CreateTaskAsync(TaskDto taskDto)
        {
            var httpClient = _clientFactory.CreateClient(_httpClientName);
            var response = await httpClient.PostAsJsonAsync(string.Empty, taskDto);

            var createdTask = await response.Content.ReadFromJsonAsync<TaskDto>();

            return createdTask;
        }

        public async Task UpdateTaskAsync(TaskDto taskDto)
        {
            var httpClient = _clientFactory.CreateClient(_httpClientName);
            var uri = $"{taskDto.Id}";

            await httpClient.PutAsJsonAsync(uri, taskDto);
        }

        public async Task UpdateTasksAsync(List<TaskDto> taskDtos)
        {
            var httpClient = _clientFactory.CreateClient(_httpClientName);
            await httpClient.PutAsJsonAsync(string.Empty, taskDtos);
        }

        public async Task DeleteTaskAsync(TaskDto taskDto)
        {
            var httpClient = _clientFactory.CreateClient(_httpClientName);
            var uri = $"{taskDto.Id}";

            await httpClient.DeleteAsync(uri);
        }
    }
}
