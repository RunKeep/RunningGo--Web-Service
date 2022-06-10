using Microsoft.EntityFrameworkCore;
using RunningGo.API.Shared.Domain.Models;
using RunningGo.API.Shared.Domain.Repositories;
using RunningGo.API.Shared.Persistence.Contexts;

namespace RunningGo.API.Shared.Persistence.Repositories;

public class UserRepository: BaseRepository, IUserRepository
{
    public UserRepository(EnhancedDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<User>> List()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task Add(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<User> FindById(long id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User> FindByFullName(string name, string lastName)
    {
        return await _context.Users
            .FirstOrDefaultAsync(p => p.Name == name && p.LastName == lastName);
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
    }

    public void Remove(User user)
    {
        _context.Users.Remove(user);
    }
}