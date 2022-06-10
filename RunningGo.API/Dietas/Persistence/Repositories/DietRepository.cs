using Microsoft.EntityFrameworkCore;
using RunningGo.API.Dietas.Domain.Models;
using RunningGo.API.Dietas.Domain.Repositories;
using RunningGo.API.Shared.Persistence.Contexts;
using RunningGo.API.Shared.Persistence.Repositories;

namespace RunningGo.API.Dietas.Persistence.Repositories;

public class DietRepository: BaseRepository, IDietRepository
{
    public DietRepository(EnhancedDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Diet>> List()
    {
        return await _context.Diets.Include(p=> p.Food).
            ToListAsync();
    }

    public async Task Add(Diet diet)
    {
        await _context.Diets.AddAsync(diet);
    }

    public async Task<Diet> FindById(int id)
    {
        return await _context.Diets.FindAsync(id);
    }

    public void Update(Diet diet)
    {
        _context.Diets.Update(diet);
    }

    public void Remove(Diet diet)
    {
        _context.Diets.Remove(diet);
    }
}