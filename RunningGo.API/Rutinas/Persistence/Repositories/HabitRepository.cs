using Microsoft.EntityFrameworkCore;
using RunningGo.API.Rutinas.Domain.Models;
using RunningGo.API.Rutinas.Domain.Repositories;
using RunningGo.API.Shared.Persistence.Contexts;
using RunningGo.API.Shared.Persistence.Repositories;

namespace RunningGo.API.Rutinas.Persistence.Repositories;

public class HabitRepository: BaseRepository, IHabitRepository
{
    public HabitRepository(EnhancedDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Habit>> List()
    {
        return await _context.Habits.ToListAsync();
    }

    public async Task Add(Habit habit)
    {
        await _context.Habits.AddAsync(habit);
    }

    public async Task<Habit> FindById(int id)
    {
        return await _context.Habits.FindAsync(id);
    }

    public void Update(Habit habit)
    {
        _context.Habits.Update(habit);
    }

    public void Remove(Habit habit)
    {
        _context.Habits.Remove(habit);
    }
}