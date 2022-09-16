using TaskOrganizer.Shared;

namespace TaskOrganizer.AppServer.Services
{
    public class TaskBoardService : ITaskBoardService
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string _httpClientName = "TaskBoards";

        public TaskBoardService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<List<TaskBoardDto>> GetTaskBoardsAsync()
        {
            var httpClient = _clientFactory.CreateClient(_httpClientName);

            var taskBoards = await httpClient.GetFromJsonAsync<List<TaskBoardDto>>(string.Empty);

            return taskBoards ?? new List<TaskBoardDto>();
        }

        public async Task<TaskBoardDto> CreateTaskBoardAsync(TaskBoardDto taskBoardDto)
        {
            var httpClient = _clientFactory.CreateClient(_httpClientName);
            var response = await httpClient.PostAsJsonAsync(string.Empty, taskBoardDto);

            var createdTaskBoard = await response.Content.ReadFromJsonAsync<TaskBoardDto>();

            return createdTaskBoard;
        }

        public async Task UpdateTaskBoardAsync(TaskBoardDto taskBoardDto)
        {
            var httpClient = _clientFactory.CreateClient(_httpClientName);
            var uri = $"{taskBoardDto.Id}";

            await httpClient.PutAsJsonAsync(uri, taskBoardDto);
        }

        public async Task DeleteTaskBoardAsync(TaskBoardDto taskBoardDto)
        {
            var httpClient = _clientFactory.CreateClient(_httpClientName);
            var uri = $"{taskBoardDto.Id}";

            await httpClient.DeleteAsync(uri);
        }
    }
}
