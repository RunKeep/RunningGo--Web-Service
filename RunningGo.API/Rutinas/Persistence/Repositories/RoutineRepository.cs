using Microsoft.EntityFrameworkCore;
using RunningGo.API.Rutinas.Domain.Models;
using RunningGo.API.Rutinas.Domain.Repositories;
using RunningGo.API.Shared.Persistence.Contexts;
using RunningGo.API.Shared.Persistence.Repositories;

namespace RunningGo.API.Rutinas.Persistence.Repositories;

public class RoutineRepository: BaseRepository, IRoutineRepository
{
    public RoutineRepository(EnhancedDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Routine>> List()
    {
        return await _context.Routines.Include(p => p.Habit)
            .ToListAsync();
    }

    public async Task<IEnumerable<Routine>> ListByUserId(long userId)
    {
        return await _context.Routines
            .Where(p => p.UserId == userId)
            .Include(p => p.Habit)
            .ToListAsync();
    }

    public async Task<Routine> FindById(long id)
    {
        return await _context.Routines
            .Include(p => p.Habit)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Routine> FindByName(string name)
    {
        return await _context.Routines.Include(p => p.Habit)
            .FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task<Routine> FindByNameAndUserId(string name, long userId)
    {
        return await _context.Routines.Include(p => p.Habit)
            .FirstOrDefaultAsync(p => p.Name == name && p.UserId == userId);
    }

    public async Task Add(Routine routine)
    {
        await _context.Routines.AddAsync(routine);
    }

    public void Update(Routine routine)
    {
        _context.Routines.Update(routine);
    }

    public void Remove(Routine routine)
    {
        _context.Routines.Remove(routine);
    }
}