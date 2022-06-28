using RunningGo.API.Checkeos.Domain.Models;

namespace RunningGo.API.Checkeos.Domain.Repositories;

public interface ISpecialistRepository
{
    Task<IEnumerable<Specialist>> List();
    Task<Specialist> FindById(long id);
    Task<Specialist> FindByName(string name);
    Task Add(Specialist specialist);
    void Update(Specialist specialist);
    void Remove(Specialist specialist);
}