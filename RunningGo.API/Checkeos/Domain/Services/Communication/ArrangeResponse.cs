using RunningGo.API.Checkeos.Domain.Models;
using RunningGo.API.Shared.Domain.Services.Communication;

namespace RunningGo.API.Checkeos.Domain.Services.Communication;

public class ArrangeResponse: BaseResponse<Arrange>
{
    public ArrangeResponse(string message) : base(message)
    {
    }

    public ArrangeResponse(Arrange resource) : base(resource)
    {
    }
}