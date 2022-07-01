using RunningGo.API.Shared.Domain.Services.Communication;
using RunningGo.API.SistemaDeMetas.Domain.Models;

namespace RunningGo.API.SistemaDeMetas.Domain.Services.Communication;

public class ProcessResponse: BaseResponse<Process>
{
    public ProcessResponse(string message) : base(message)
    {
    }

    public ProcessResponse(Process resource) : base(resource)
    {
    }
}