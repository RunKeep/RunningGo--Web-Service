using RunningGo.API.Rutinas.Domain.Models;
using RunningGo.API.Rutinas.Domain.Repositories;
using RunningGo.API.Rutinas.Domain.Services;
using RunningGo.API.Rutinas.Domain.Services.Communication;
using RunningGo.API.Shared.Domain.Repositories;

namespace RunningGo.API.Rutinas.Services;

public class HabitService: IHabitService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHabitRepository _habitRepository;

    public HabitService(IUnitOfWork unitOfWork, IHabitRepository habitRepository)
    {
        _unitOfWork = unitOfWork;
        _habitRepository = habitRepository;
    }

    public async Task<IEnumerable<Habit>> List()
    {
        return await _habitRepository.List();
    }

    public async Task<HabitResponse> FindById(int id)
    {
        var habit = await _habitRepository.FindById(id);
        if (habit == null)
            return new HabitResponse("Habit doesn't exist");
        return new HabitResponse(habit);
    }

    public async Task<HabitResponse> Save(Habit model)
    {
        var existingHabitWithDescription = await _habitRepository.FindByDescription(model.Description);

        if (existingHabitWithDescription != null)
            return new HabitResponse($"Habit with description already exists.");

        try
        {
            await _habitRepository.Add(model);
            await _unitOfWork.Complete();
            return new HabitResponse(model);
        }
        catch (Exception e)
        {
            return new HabitResponse($"An exception occurred while adding habit: {e.Message}");
        }

    }

    public async Task<HabitResponse> Update(int id, Habit model)
    {
        var existingHabit = await _habitRepository.FindById(id);
        if (existingHabit == null)
            return new HabitResponse("This habit isn't registered. Please add it.");
        
        var existingHabitWithDescription = _habitRepository.FindByDescription(model.Description);

        if (existingHabitWithDescription != null && existingHabitWithDescription.Id != id)
            return new HabitResponse($"Habit with description already exists.");

        existingHabit.Description = model.Description;
        
        try
        {
            _habitRepository.Update(existingHabit);
            await _unitOfWork.Complete();
            return new HabitResponse(existingHabit);
        }
        catch (Exception e)
        {
            return new HabitResponse($"An exception occurred while updating habit: {e.Message}");
        }
    }

    public async Task<HabitResponse> Delete(int id)
    {
        var habit = await _habitRepository.FindById(id);
        if (habit == null)
            return new HabitResponse("This habit doesn't exist.");
        
        try
        {
            _habitRepository.Remove(habit);
            await _unitOfWork.Complete();
            return new HabitResponse(habit);
        }
        catch (Exception e)
        {
            return new HabitResponse($"An error occurred while deleting habit: {e.Message}");
        }
    }
}