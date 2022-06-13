using RunningGo.API.Rutinas.Domain.Models;

namespace RunningGo.API.Rutinas.Domain.Repositories;

public interface IHabitRepository
{
    Task<IEnumerable<Habit>> List();
    Task Add(Habit habit);
    Task<Habit> FindById(int id);
    void Update(Habit habit);
    void Remove(Habit habit);
}