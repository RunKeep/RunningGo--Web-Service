using RunningGo.API.Dietas.Domain.Models;
using RunningGo.API.Dietas.Domain.Repositories;
using RunningGo.API.Dietas.Domain.Services;
using RunningGo.API.Dietas.Domain.Services.Communication;
using RunningGo.API.Shared.Domain.Repositories;

namespace RunningGo.API.Dietas.Services;

public class DietService: IDietService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDietRepository _dietRepository;

    public DietService(IUnitOfWork unitOfWork, IDietRepository dietRepository)
    {
        _unitOfWork = unitOfWork;
        _dietRepository = dietRepository;
    }

    public async Task<IEnumerable<Diet>> List()
    {
        return await _dietRepository.List();
    }

    public async Task<DietResponse> Save(Diet model)
    {
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
        if (existingDiet != null)
            return new DietResponse("This diet doesn't exist. Please create it.");

        existingDiet.Description = model.Description;
        existingDiet.Specs = model.Specs;
        existingDiet.Duration = model.Duration;

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
        if (existingDiet != null)
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