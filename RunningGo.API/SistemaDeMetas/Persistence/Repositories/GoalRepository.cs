using Microsoft.EntityFrameworkCore;
using RunningGo.API.Shared.Persistence.Contexts;
using RunningGo.API.Shared.Persistence.Repositories;
using RunningGo.API.SistemaDeMetas.Domain.Models;
using RunningGo.API.SistemaDeMetas.Domain.Repositories;

namespace RunningGo.API.SistemaDeMetas.Persistence.Repositories;

public class GoalRepository: BaseRepository, IGoalRepository
{
    public GoalRepository(EnhancedDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Goal>> List()
    {
        return await _context.Goals.ToListAsync();
    }

    public async Task Add(Goal goal)
    {
        await _context.Goals.AddAsync(goal);
    }

    public async Task<Goal> FindById(int id)
    {
        return await _context.Goals.FindAsync(id);
    }

    public async Task<Goal> FindByDescription(string description)
    {
        return await _context.Goals.FirstOrDefaultAsync(p => p.Description == description);
    }

    public void Update(Goal goal)
    {
        _context.Goals.Update(goal);
    }

    public void Remove(Goal goal)
    {
        _context.Goals.Remove(goal);
    }
}