using RunningGo.API.Shared.Domain.Services.Communication;
using RunningGo.API.SistemaDeMetas.Domain.Models;

namespace RunningGo.API.SistemaDeMetas.Domain.Services.Communication;

public class GoalResponse: BaseResponse<Goal>
{
    public GoalResponse(string message) : base(message)
    {
    }

    public GoalResponse(Goal resource) : base(resource)
    {
    }
}