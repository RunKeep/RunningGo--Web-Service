using RunningGo.API.Checkeos.Domain.Models;
using RunningGo.API.Checkeos.Domain.Services.Communication;
using RunningGo.API.Shared.Domain.Services;

namespace RunningGo.API.Checkeos.Domain.Services;

public interface ICheckupService: IBaseService<Checkup, CheckupResponse, int>
{
    Task<CheckupResponse> FindById(int id);
    Task<IEnumerable<Checkup>> ListByUserId(long userId);
    Task<IEnumerable<Checkup>> ListBySpecialistId(long specialistId);
}