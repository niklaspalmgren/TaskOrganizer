using TaskOrganizer.Shared;

namespace TaskOrganizer.AppServer.Services;

public class UserService : IUserService
{
    private readonly IHttpClientFactory _clientFactory;
    private const string _httpClientName = "Users";

    public UserService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<List<UserDto>> GetUsersAsync()
    {
        var httpClient = _clientFactory.CreateClient(_httpClientName);
        var users = await httpClient.GetFromJsonAsync<List<UserDto>>(string.Empty);

        return users ?? new List<UserDto>();
    }

    public async Task<UserDto?> GetUserAsync(int id)
    {
        var httpClient = _clientFactory.CreateClient(_httpClientName);
        var uri = $"{id}";
        var user = await httpClient.GetFromJsonAsync<UserDto>(uri);

        return user;
    }
    
    public async Task<UserDto?> GetUserAsync(int? id)
    {
        if (id is null)
            return null;
        
        var httpClient = _clientFactory.CreateClient(_httpClientName);
        var uri = $"{(int)id}";
        var user = await httpClient.GetFromJsonAsync<UserDto>(uri);

        return user;
    }
    

    public async Task<UserDto?> CreateUserAsync(UserDto userDto)
    {
        var httpClient = _clientFactory.CreateClient(_httpClientName);
        var response = await httpClient.PostAsJsonAsync(string.Empty, userDto);

        var createdTask = await response.Content.ReadFromJsonAsync<UserDto>();

        return createdTask;
    }

    public async Task UpdateUserAsync(UserDto userDto)
    {
        var httpClient = _clientFactory.CreateClient(_httpClientName);
        var uri = $"{userDto.Id}";

        await httpClient.PutAsJsonAsync(uri, userDto);
    }

    public async Task DeleteUserAsync(UserDto userDto)
    {
        var httpClient = _clientFactory.CreateClient(_httpClientName);
        var uri = $"{userDto.Id}";

        await httpClient.DeleteAsync(uri);
    }
}