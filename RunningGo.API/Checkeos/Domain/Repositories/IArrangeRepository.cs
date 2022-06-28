using RunningGo.API.Checkeos.Domain.Models;

namespace RunningGo.API.Checkeos.Domain.Repositories;

public interface IArrangeRepository
{
    Task<IEnumerable<Arrange>> List();
    Task<Arrange> FindById(int id);
    Task Add(Arrange arrange);
    void Update(Arrange arrange);
    void Remove(Arrange arrange);
}