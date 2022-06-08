using RunningGo.API.Dietas.Domain.Models;
using RunningGo.API.Dietas.Domain.Repositories;
using RunningGo.API.Dietas.Domain.Services;
using RunningGo.API.Dietas.Domain.Services.Communication;
using RunningGo.API.Shared.Domain.Repositories;

namespace RunningGo.API.Dietas.Services;

public class FoodService: IFoodService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFoodRepository _foodRepository;

    public FoodService(IUnitOfWork unitOfWork, IFoodRepository foodRepository)
    {
        _unitOfWork = unitOfWork;
        _foodRepository = foodRepository;
    }


    public async Task<IEnumerable<Food>> List()
    {
        return await _foodRepository.List();
    }

    public async Task<FoodResponse> Save(Food model)
    {
        try
        {
            await _foodRepository.Add(model);
            await _unitOfWork.Complete();
            return new FoodResponse(model);
        }
        catch (Exception e)
        {
            return new FoodResponse($"An exception ocurred while adding food: {e.Message}");
        }
    }

    public async Task<FoodResponse> Update(int id, Food model)
    {
        var existingFood = await _foodRepository.FindById(id);
        if (existingFood == null)
            return new FoodResponse("This food isn't registered. Please add it.");

        existingFood.Name = model.Name;
        existingFood.Calories = model.Calories;
        existingFood.Vitamins = model.Vitamins;
        existingFood.Quantity = model.Quantity;
        
        try
        {
            _foodRepository.Update(existingFood);
            await _unitOfWork.Complete();
            return new FoodResponse(existingFood);
        }
        catch (Exception e)
        {
            return new FoodResponse($"An exception ocurred while updating food: {e.Message}");
        }
    }

    public async Task<FoodResponse> Delete(int id)
    {
        var food = await _foodRepository.FindById(id);
        if (food == null)
            return new FoodResponse("This food isn't registered.");
        return new FoodResponse(food);
    }
}