using Microsoft.EntityFrameworkCore;
using RunningGo.API.Shared.Persistence.Contexts;
using RunningGo.API.Shared.Persistence.Repositories;
using RunningGo.API.SistemaDeMetas.Domain.Models;
using RunningGo.API.SistemaDeMetas.Domain.Repositories;

namespace RunningGo.API.SistemaDeMetas.Persistence.Repositories;

public class ProcessRepository: BaseRepository, IProcessRepository
{
    public ProcessRepository(EnhancedDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Process>> List()
    {
        return await _context.Processes.Include(p => p.Goals).ToListAsync();
    }

    public async Task<Process> FindById(int id)
    {
        return await _context.Processes
            .Include(p => p.Goals)
            .FirstOrDefaultAsync(p=>p.Id==id);
    }

    public async Task<Process> FindByDescription(string description)
    {
        return await _context.Processes.FirstOrDefaultAsync(p => p.Description == description);
    }

    public async Task<IEnumerable<Process>> ListByUserId(long userId)
    {
        return await _context.Processes.Where(p => p.UserId == userId)
            .Include(p => p.Goals)
            .ToListAsync();
    }

    public async Task Add(Process process)
    {
        await _context.Processes.AddAsync(process);
    }

    public void Update(Process process)
    {
        _context.Processes.Update(process);
    }

    public void Remove(Process process)
    {
        _context.Processes.Remove(process);
    }
}