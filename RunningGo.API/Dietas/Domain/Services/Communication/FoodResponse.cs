using RunningGo.API.Dietas.Domain.Models;
using RunningGo.API.Shared.Domain.Services.Communication;

namespace RunningGo.API.Dietas.Domain.Services.Communication;

public class FoodResponse: BaseResponse<Food>
{
    public FoodResponse(string message): base(message) { }
    public FoodResponse(Food resource): base(resource) { }
}