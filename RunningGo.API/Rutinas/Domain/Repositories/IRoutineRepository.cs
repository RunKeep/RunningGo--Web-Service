using RunningGo.API.Rutinas.Domain.Models;

namespace RunningGo.API.Rutinas.Domain.Repositories;

public interface IRoutineRepository
{
    Task<IEnumerable<Routine>> List();
    Task<IEnumerable<Routine>> ListByUserId(long userId);
    Task<Routine> FindById(long id);
    Task<Routine> FindByName(string name);
    Task<Routine> FindByNameAndUserId(string name, long userId);
    Task Add(Routine routine);
    void Update(Routine routine);
    void Remove(Routine routine);
}