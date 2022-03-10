using WebStep.Dto;

namespace webStep.AppServer.Data
{
    public class TaskBoardService : ITaskBoardService
    {
        private readonly HttpClient _client;

        public TaskBoardService(HttpClient httpClient)
        {
            _client = httpClient;
        }

        public async Task<List<TaskBoardDto>> GetAllTaskBoardsAsync()
        {
            var uri = "api/taskboards";
            var tasks = await _client.GetFromJsonAsync<List<TaskBoardDto>>(uri);

            return tasks ?? new List<TaskBoardDto>();
        }
    }
}
