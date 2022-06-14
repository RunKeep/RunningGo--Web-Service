using RunningGo.API.Rutinas.Domain.Models;
using RunningGo.API.Shared.Domain.Services.Communication;

namespace RunningGo.API.Rutinas.Domain.Services.Communication;

public class HabitResponse: BaseResponse<Habit>
{
    public HabitResponse(string message) : base(message)
    {
    }

    public HabitResponse(Habit resource) : base(resource)
    {
    }
}