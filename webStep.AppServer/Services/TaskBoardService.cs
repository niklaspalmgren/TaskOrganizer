using WebStep.Dto;

namespace webStep.AppServer.Services
{
    public class TaskBoardService : ITaskBoardService
    {
        private readonly IHttpClientFactory _clientFactory;

        public TaskBoardService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<List<TaskBoardDto>> GetAllTaskBoardsAsync()
        {
            var httpClient = _clientFactory.CreateClient("TaskBoards");
            var tasks = await httpClient.GetFromJsonAsync<List<TaskBoardDto>>(string.Empty);

            return tasks ?? new List<TaskBoardDto>();
        }

        public async Task<TaskBoardDto> CreateTaskBoardAsync(TaskBoardDto taskBoardDto)
        {
            var httpClient = _clientFactory.CreateClient("TaskBoards");
            var response = await httpClient.PostAsJsonAsync(string.Empty, taskBoardDto);

            var createdTaskBoard = await response.Content.ReadFromJsonAsync<TaskBoardDto>();

            return createdTaskBoard;
        }

        public async Task UpdateTaskBoardAsync(TaskBoardDto taskBoardDto)
        {
            var httpClient = _clientFactory.CreateClient("TaskBoards");
            var uri = $"{taskBoardDto.Id}";

            await httpClient.PutAsJsonAsync(uri, taskBoardDto);
        }

        public async Task DeleteTaskBoardAsync(TaskBoardDto taskBoardDto)
        {
            var httpClient = _clientFactory.CreateClient("TaskBoards");
            var uri = $"{taskBoardDto.Id}";

            await httpClient.DeleteAsync(uri);
        }
    }
}
