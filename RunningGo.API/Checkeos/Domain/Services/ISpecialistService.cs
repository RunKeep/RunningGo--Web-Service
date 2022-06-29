using RunningGo.API.Checkeos.Domain.Models;
using RunningGo.API.Checkeos.Domain.Services.Communication;
using RunningGo.API.Shared.Domain.Services;

namespace RunningGo.API.Checkeos.Domain.Services;

public interface ISpecialistService: IBaseService<Specialist, SpecialistResponse, long>
{
    Task<SpecialistResponse> FindById(long id);
}