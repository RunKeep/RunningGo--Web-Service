using RunningGo.API.Shared.Domain.Repositories;
using RunningGo.API.SistemaDeMetas.Domain.Models;
using RunningGo.API.SistemaDeMetas.Domain.Repositories;
using RunningGo.API.SistemaDeMetas.Domain.Services;
using RunningGo.API.SistemaDeMetas.Domain.Services.Communication;

namespace RunningGo.API.SistemaDeMetas.Services;

public class GoalService: IGoalService
{
    private readonly IGoalRepository _goalRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GoalService(IGoalRepository goalRepository, IUnitOfWork unitOfWork)
    {
        _goalRepository = goalRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Goal>> List()
    {
        return await _goalRepository.List();
    }

    public async Task<GoalResponse> Save(Goal model)
    {
        var existingGoalWithName = await _goalRepository.FindByDescription(model.Description);
        if (existingGoalWithName != null)
            return new GoalResponse($"A goal with description {model.Description} already exists.");
        try
        {
            await _goalRepository.Add(model);
            await _unitOfWork.Complete();
            return new GoalResponse(model);
        }
        catch (Exception e)
        {
            return new GoalResponse($"An exception occurred while saving goal: {e.Message}");
        }
    }

    public async Task<GoalResponse> Update(int id, Goal model)
    {
        var existingGoal = await _goalRepository.FindById(id);
        if (existingGoal == null)
            return new GoalResponse("Goal doesn't exist. Please create it.");
        
        var existingGoalWithName = await _goalRepository.FindByDescription(model.Description);
        if (existingGoalWithName != null && existingGoalWithName.Id != id)
            return new GoalResponse($"A goal with description {model.Description} already exists.");

        existingGoal.Description = model.Description;
        existingGoal.Quantity = model.Quantity;

        try
        {
            _goalRepository.Update(existingGoal);
            await _unitOfWork.Complete();
            return new GoalResponse(existingGoal);
        }
        catch (Exception e)
        {
            return new GoalResponse($"An exception occurred while updating goal: {e.Message}");
        }
    }

    public async Task<GoalResponse> Delete(int id)
    {
        var existingGoal = await _goalRepository.FindById(id);
        if (existingGoal == null)
            return new GoalResponse("Goal doesn't exist");
        try
        {
            _goalRepository.Remove(existingGoal);
            await _unitOfWork.Complete();
            return new GoalResponse(existingGoal);
        }
        catch (Exception e)
        {
            return new GoalResponse($"An exception occurred while deleting goal: {e.Message}");
        }
    }

    public async Task<GoalResponse> FindById(int id)
    {
        var goal = await _goalRepository.FindById(id);
        if (goal == null)
            return new GoalResponse("Goal doesn't exist");
        return new GoalResponse(goal);
    }
}