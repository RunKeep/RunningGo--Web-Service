using Microsoft.EntityFrameworkCore;
using RunningGo.API.Checkeos.Domain.Models;
using RunningGo.API.Checkeos.Domain.Repositories;
using RunningGo.API.Shared.Persistence.Contexts;
using RunningGo.API.Shared.Persistence.Repositories;

namespace RunningGo.API.Checkeos.Persistence.Repositories;

public class SpecialistRepository: BaseRepository, ISpecialistRepository
{
    public SpecialistRepository(EnhancedDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Specialist>> List()
    {
        return await _context.Specialists.ToListAsync();
    }

    public async Task<Specialist> FindById(long id)
    {
        return await _context.Specialists.FindAsync(id);
    }

    public async Task<Specialist> FindByName(string name)
    {
        return await _context.Specialists.FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task Add(Specialist specialist)
    {
        await _context.Specialists.AddAsync(specialist);
    }

    public void Update(Specialist specialist)
    {
        _context.Specialists.Update(specialist);
    }

    public void Remove(Specialist specialist)
    {
        _context.Specialists.Remove(specialist);
    }
}