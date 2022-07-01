using RunningGo.API.Shared.Domain.Repositories;
using RunningGo.API.SistemaDeMetas.Domain.Models;
using RunningGo.API.SistemaDeMetas.Domain.Repositories;
using RunningGo.API.SistemaDeMetas.Domain.Services;
using RunningGo.API.SistemaDeMetas.Domain.Services.Communication;

namespace RunningGo.API.SistemaDeMetas.Services;

public class GoalService: IGoalService
{
    private readonly IGoalRepository _goalRepository;
    private readonly IProcessRepository _processRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GoalService(IGoalRepository goalRepository, IUnitOfWork unitOfWork, IProcessRepository processRepository)
    {
        _goalRepository = goalRepository;
        _unitOfWork = unitOfWork;
        _processRepository = processRepository;
    }

    public async Task<IEnumerable<Goal>> List()
    {
        return await _goalRepository.List();
    }

    public async Task<GoalResponse> Save(Goal model)
    {
        var existingProcess = await _processRepository.FindById(model.ProcessId);
        if (existingProcess == null)
            return new GoalResponse($"Process with id {model.ProcessId} doesn't exist.");
        
        var existingGoalWithDescription = await _goalRepository.FindByDescription(model.Description);
        if (existingGoalWithDescription != null && existingGoalWithDescription.ProcessId == model.ProcessId)
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
        
        var existingProcess = await _processRepository.FindById(model.ProcessId);
        if (existingProcess == null)
            return new GoalResponse($"Process with id {model.ProcessId} doesn't exist.");
        
        var existingGoalWithDescription = await _goalRepository.FindByDescription(model.Description);
        if (existingGoalWithDescription != null && existingGoalWithDescription.Id != id && existingGoalWithDescription.ProcessId == existingGoal.ProcessId)
            return new GoalResponse($"A goal with description {model.Description} already exists.");

        existingGoal.Description = model.Description;
        existingGoal.Quantity = model.Quantity;
        existingGoal.End = model.End;

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