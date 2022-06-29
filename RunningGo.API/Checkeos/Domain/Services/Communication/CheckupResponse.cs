using RunningGo.API.Checkeos.Domain.Models;
using RunningGo.API.Shared.Domain.Services.Communication;

namespace RunningGo.API.Checkeos.Domain.Services.Communication;

public class CheckupResponse: BaseResponse<Checkup>
{
    public CheckupResponse(string message) : base(message)
    {
    }

    public CheckupResponse(Checkup resource) : base(resource)
    {
    }
}