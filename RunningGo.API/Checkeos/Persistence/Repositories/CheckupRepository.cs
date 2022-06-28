using Microsoft.EntityFrameworkCore;
using RunningGo.API.Checkeos.Domain.Models;
using RunningGo.API.Checkeos.Domain.Repositories;
using RunningGo.API.Shared.Persistence.Contexts;
using RunningGo.API.Shared.Persistence.Repositories;

namespace RunningGo.API.Checkeos.Persistence.Repositories;

public class CheckupRepository: BaseRepository, ICheckupRepository
{
    public CheckupRepository(EnhancedDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Checkup>> List()
    {
        return await _context.Checkups
            .Include(p => p.Specialist)
            .Include(p => p.Arrange)
            .ToListAsync();
    }

    public async Task<IEnumerable<Checkup>> ListByUserId(long userId)
    {
        return await _context.Checkups.Where(p => p.UserId == userId)
            .Include(p => p.Specialist)
            .Include(p => p.Arrange)
            .ToListAsync();
    }

    public async Task<IEnumerable<Checkup>> ListBySpecialistId(long specialistId)
    {
        return await _context.Checkups.Where(p => p.SpecialistId == specialistId)
            .Include(p => p.Specialist)
            .Include(p => p.Arrange)
            .ToListAsync();
    }

    public async Task<Checkup> FindById(int id)
    {
        return await _context.Checkups
            .Include(p => p.Specialist)
            .Include(p => p.Arrange)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task Add(Checkup checkup)
    {
        await _context.Checkups.AddAsync(checkup);
    }

    public void Update(Checkup checkup)
    {
        _context.Checkups.Update(checkup);
    }

    public void Remove(Checkup checkup)
    {
        _context.Checkups.Remove(checkup);
    }
}