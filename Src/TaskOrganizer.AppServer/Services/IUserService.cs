using TaskOrganizer.Shared;

namespace TaskOrganizer.AppServer.Services;

public interface IUserService
{
    Task<List<UserDto>> GetUsersAsync();
    Task<UserDto?> GetUserAsync(int id);
    Task<UserDto?> GetUserAsync(int? id);
    Task<UserDto?> CreateUserAsync(UserDto userDto);
    Task UpdateUserAsync(UserDto userDto);
    Task DeleteUserAsync(UserDto userDto);
}