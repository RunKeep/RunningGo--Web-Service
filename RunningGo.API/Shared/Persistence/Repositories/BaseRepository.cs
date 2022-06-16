using RunningGo.API.Shared.Persistence.Contexts;

namespace RunningGo.API.Shared.Persistence.Repositories;

public class BaseRepository
{
    protected readonly EnhancedDbContext _context;

    public BaseRepository(EnhancedDbContext context)
    {
        _context = context;
    }
}