using RunningGo.API.Shared.Domain.Repositories;
using RunningGo.API.Shared.Persistence.Contexts;

namespace RunningGo.API.Shared.Persistence.Repositories;

public class UnitOfWork: IUnitOfWork
{
    private readonly EnhancedDbContext _context;

    public UnitOfWork(EnhancedDbContext context)
    {
        _context = context;
    }

    public async Task Complete()
    {
        await _context.SaveChangesAsync();
    }
}