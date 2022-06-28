using Microsoft.EntityFrameworkCore;
using RunningGo.API.Checkeos.Domain.Models;
using RunningGo.API.Checkeos.Domain.Repositories;
using RunningGo.API.Shared.Persistence.Contexts;
using RunningGo.API.Shared.Persistence.Repositories;

namespace RunningGo.API.Checkeos.Persistence.Repositories;

public class ArrangeRepository: BaseRepository, IArrangeRepository
{
    public ArrangeRepository(EnhancedDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Arrange>> List()
    {
        return await _context.Arranges.ToListAsync();
    }

    public async Task<Arrange> FindById(int id)
    {
        return await _context.Arranges.FindAsync(id);
    }

    public async Task Add(Arrange arrange)
    {
        await _context.Arranges.AddAsync(arrange);
    }

    public void Update(Arrange arrange)
    {
        _context.Arranges.Update(arrange);
    }

    public void Remove(Arrange arrange)
    {
        _context.Arranges.Remove(arrange);
    }
}