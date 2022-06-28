using RunningGo.API.Checkeos.Domain.Models;
using RunningGo.API.Shared.Domain.Services.Communication;

namespace RunningGo.API.Checkeos.Domain.Services.Communication;

public class SpecialistResponse: BaseResponse<Specialist>
{
    public SpecialistResponse(string message) : base(message)
    {
    }

    public SpecialistResponse(Specialist resource) : base(resource)
    {
    }
}