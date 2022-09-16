using Microsoft.EntityFrameworkCore;
using TaskOrganizer.Api.Data;
using TaskOrganizer.Api.Entities;
using Task = System.Threading.Tasks.Task;

namespace TaskOrganizer.Api.Services;

public class UserRepo : IUserRepo
{
    private readonly TasksDb _context;

    public UserRepo(TasksDb context)
    {
        _context = context;
    }

    public async Task<List<User>> GetUsersAsync()
    {
        var users = await _context.Users.Include(x => x.Tasks).ToListAsync();
        return users;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        var user = await _context.Users.Include( x => x.Tasks).FirstOrDefaultAsync(x => x.Id == id);
        return user;
    }

    public void AddUser(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));
        
        _context.Users.Add(user);
    }

    public void DeleteUser(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        _context.Users.Remove(user);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}