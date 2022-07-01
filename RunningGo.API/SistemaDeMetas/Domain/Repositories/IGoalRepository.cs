using RunningGo.API.SistemaDeMetas.Domain.Models;

namespace RunningGo.API.SistemaDeMetas.Domain.Repositories;

public interface IGoalRepository
{
    Task<IEnumerable<Goal>> List();
    Task Add(Goal goal);
    Task<Goal> FindById(int id);
    Task<Goal> FindByDescription(string description);
    void Update(Goal goal);
    void Remove(Goal goal);
}