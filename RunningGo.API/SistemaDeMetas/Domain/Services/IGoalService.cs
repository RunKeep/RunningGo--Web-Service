using RunningGo.API.Shared.Domain.Services;
using RunningGo.API.SistemaDeMetas.Domain.Models;
using RunningGo.API.SistemaDeMetas.Domain.Services.Communication;

namespace RunningGo.API.SistemaDeMetas.Domain.Services;

public interface IGoalService: IBaseService<Goal, GoalResponse, int>
{
    Task<GoalResponse> FindById(int id);
}