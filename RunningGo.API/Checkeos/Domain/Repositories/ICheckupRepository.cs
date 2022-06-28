using RunningGo.API.Checkeos.Domain.Models;

namespace RunningGo.API.Checkeos.Domain.Repositories;

public interface ICheckupRepository
{
    Task<IEnumerable<Checkup>> List();
    Task<IEnumerable<Checkup>> ListByUserId(long userId);
    Task<IEnumerable<Checkup>> ListBySpecialistId(long specialistId);
    Task<Checkup> FindById(int id);
    Task Add(Checkup checkup);
    void Update(Checkup checkup);
    void Remove(Checkup checkup);
}