using RunningGo.API.Dietas.Domain.Models;
using RunningGo.API.Dietas.Domain.Services.Communication;
using RunningGo.API.Shared.Domain.Services;

namespace RunningGo.API.Dietas.Domain.Services;

public interface IFoodService: IBaseService<Food, FoodResponse, int>
{
    Task<FoodResponse> FindById(int id);
}