using RunningGo.API.SistemaDeMetas.Domain.Models;

namespace RunningGo.API.SistemaDeMetas.Domain.Repositories;

public interface IProcessRepository
{
    Task<IEnumerable<Process>> List();
    Task<Process> FindById(int id);
    Task<Process> FindByDescription(string description);
    Task<IEnumerable<Process>> ListByUserId(long userId);
    Task Add(Process process);
    void Update(Process process);
    void Remove(Process process);
}