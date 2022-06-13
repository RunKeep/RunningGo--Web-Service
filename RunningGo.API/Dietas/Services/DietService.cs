using RunningGo.API.Dietas.Domain.Models;
using RunningGo.API.Dietas.Domain.Repositories;
using RunningGo.API.Dietas.Domain.Services;
using RunningGo.API.Dietas.Domain.Services.Communication;
using RunningGo.API.Shared.Domain.Repositories;

namespace RunningGo.API.Dietas.Services;

public class DietService: IDietService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFoodRepository _foodRepository;
    private readonly IDietRepository _dietRepository;
    private readonly IUserRepository _userRepository;

    public DietService(IUnitOfWork unitOfWork, IDietRepository dietRepository, IFoodRepository foodRepository, IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork;
        _dietRepository = dietRepository;
        _foodRepository = foodRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<Diet>> List()
    {
        return await _dietRepository.List();
    }

    public async Task<IEnumerable<Diet>> ListByUserId(long userId)
    {
        return await _dietRepository.ListByUserId(userId);
    }
    
    public async Task<DietResponse> Save(Diet model)
    {
        var food = await _foodRepository.FindById(model.FoodId);
        if (food == null)
            return new DietResponse($"Food with id {model.FoodId} doesn't exist. Verify if you have registered that food.");
        
        var user = await _userRepository.FindById(model.UserId);
        if (user == null)
            return new DietResponse($"User with id {model.UserId} doesn't exist. Verify if you have registered that user.");
        
        try
        {
            await _dietRepository.Add(model);
            await _unitOfWork.Complete();
            return new DietResponse(model);
        }
        catch (Exception e)
        {
            return new DietResponse($"An exception occurred while saving diet: {e.Message}");
        }
    }

    public async Task<DietResponse> Update(int id, Diet model)
    {
        var existingDiet = await _dietRepository.FindById(id);
        if (existingDiet == null)
            return new DietResponse($"Diet with id {id} doesn't exist. Please create it.");

        var food = await _foodRepository.FindById(model.FoodId);
        if (food == null)
            return new DietResponse($"Food with id {model.FoodId} doesn't exist. Verify if you have registered that user.");
        
        var user = await _userRepository.FindById(model.UserId);
        if (user == null)
            return new DietResponse($"User with id {model.UserId} doesn't exist. Verify if you have registered that user.");
        
        existingDiet.Description = model.Description;
        existingDiet.Specs = model.Specs;
        existingDiet.Duration = model.Duration;
        existingDiet.Quantity = model.Quantity;

        try
        {
            _dietRepository.Update(existingDiet);
            await _unitOfWork.Complete();
            return new DietResponse(existingDiet);
        }
        catch (Exception e)
        {
            return new DietResponse($"An exception occurred while updating diet: {e.Message}");
        }
    }

    public async Task<DietResponse> Delete(int id)
    {
        var existingDiet = await _dietRepository.FindById(id);
        if (existingDiet == null)
            return new DietResponse("Diet doesn't exist.");
        
        try
        {
            _dietRepository.Remove(existingDiet);
            await _unitOfWork.Complete();
            return new DietResponse(existingDiet);
        }
        catch (Exception e)
        {
            return new DietResponse($"An exception occurred while deleting diet: {e.Message}");
        }
    }
    
}