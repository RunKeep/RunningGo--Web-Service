using RunningGo.API.Rutinas.Domain.Models;
using RunningGo.API.Rutinas.Domain.Services.Communication;
using RunningGo.API.Shared.Domain.Services;

namespace RunningGo.API.Rutinas.Domain.Services;

public interface IRoutineService: IBaseService<Routine, RoutineResponse, long>
{
    public Task<IEnumerable<Routine>> ListByUserId(long userId);
}