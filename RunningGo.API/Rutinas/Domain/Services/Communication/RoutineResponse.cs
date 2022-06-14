using RunningGo.API.Rutinas.Domain.Models;
using RunningGo.API.Shared.Domain.Services.Communication;

namespace RunningGo.API.Rutinas.Domain.Services.Communication;

public class RoutineResponse: BaseResponse<Routine>
{
    public RoutineResponse(string message) : base(message)
    {
    }

    public RoutineResponse(Routine resource) : base(resource)
    {
    }
}