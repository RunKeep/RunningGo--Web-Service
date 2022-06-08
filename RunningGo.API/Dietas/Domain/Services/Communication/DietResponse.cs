using RunningGo.API.Dietas.Domain.Models;
using RunningGo.API.Shared.Domain.Services.Communication;

namespace RunningGo.API.Dietas.Domain.Services.Communication;

public class DietResponse: BaseResponse<Diet>
{
    public DietResponse(string message): base(message) { }
    public DietResponse(Diet resource): base(resource) { }
}