using RunningGo.API.Shared.Domain.Services;
using RunningGo.API.SistemaDeMetas.Domain.Models;
using RunningGo.API.SistemaDeMetas.Domain.Services.Communication;

namespace RunningGo.API.SistemaDeMetas.Domain.Services;

public interface IProcessService: IBaseService<Process, ProcessResponse, int>
{
    Task<ProcessResponse> FindById(int id);
    Task<IEnumerable<Process>> ListByUserId(long userId);
}