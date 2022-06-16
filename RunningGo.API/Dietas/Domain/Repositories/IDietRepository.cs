using RunningGo.API.Dietas.Domain.Models;

namespace RunningGo.API.Dietas.Domain.Repositories;

public interface IDietRepository
{
    Task<IEnumerable<Diet>> List();
    Task<IEnumerable<Diet>> ListByUserId(long userId);
    Task Add(Diet diet);
    Task<Diet> FindById(int id);
    void Update(Diet diet);
    void Remove(Diet diet);
}