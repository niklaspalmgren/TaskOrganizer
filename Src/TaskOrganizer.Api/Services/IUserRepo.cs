using TaskOrganizer.Api.Entities;
using Task = System.Threading.Tasks.Task;

namespace TaskOrganizer.Api.Services;

public interface IUserRepo
{
    public Task<List<User>> GetUsersAsync();
    public Task<User?> GetUserByIdAsync(int id);
    public void AddUser(User user);
    public void DeleteUser(User user);
    public Task SaveChangesAsync();
}