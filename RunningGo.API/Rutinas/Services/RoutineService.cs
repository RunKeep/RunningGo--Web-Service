using RunningGo.API.Rutinas.Domain.Models;
using RunningGo.API.Rutinas.Domain.Repositories;
using RunningGo.API.Rutinas.Domain.Services;
using RunningGo.API.Rutinas.Domain.Services.Communication;
using RunningGo.API.Shared.Domain.Repositories;

namespace RunningGo.API.Rutinas.Services;

public class RoutineService: IRoutineService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRoutineRepository _routineRepository;
    private readonly IHabitRepository _habitRepository;
    private readonly IUserRepository _userRepository;

    public RoutineService(IUnitOfWork unitOfWork, IRoutineRepository routineRepository, IHabitRepository habitRepository, IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork;
        _routineRepository = routineRepository;
        _habitRepository = habitRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<Routine>> List()
    {
        return await _routineRepository.List();
    }
    
    public async Task<IEnumerable<Routine>> ListByUserId(long userId)
    {
        return await _routineRepository.ListByUserId(userId);
    }

    public async Task<RoutineResponse> Save(Routine model)
    {
        var habit = await _habitRepository.FindById(model.HabitId);
        if (habit == null)
            return new RoutineResponse($"Habit with id {model.HabitId} doesn't exist. Verify if you have registered that habit");
        
        var user = await _userRepository.FindById(model.UserId);
        if (user == null)
            return new RoutineResponse($"User with id {model.UserId} doesn't exist. Verify if you have registered that user.");

        var existingRoutineWithName = _routineRepository.FindByName(model.Name);
        if (existingRoutineWithName != null)
            return new RoutineResponse($"Routine with name {model.Name} already exists.");
        
        try
        {
            await _routineRepository.Add(model);
            await _unitOfWork.Complete();
            return new RoutineResponse(model);
        }
        catch (Exception e)
        {
            return new RoutineResponse($"An exception occurred while saving routine: {e.Message}");
        }
    }

    public async Task<RoutineResponse> Update(long id, Routine model)
    {
        var existingRoutine = await _routineRepository.FindById(id);
        if (existingRoutine == null)
            return new RoutineResponse($"Routine with id {id} doesn't exist. Please add it");
        
        var habit = await _habitRepository.FindById(model.HabitId);
        if (habit == null)
            return new RoutineResponse($"Habit with id {model.HabitId} doesn't exist. Verify if you have registered that habit");
        
        var user = await _userRepository.FindById(model.UserId);
        if (user == null)
            return new RoutineResponse($"User with id {model.UserId} doesn't exist. Verify if you have registered that user.");
        
        var existingRoutineWithName = _routineRepository.FindByName(model.Name);
        if (existingRoutineWithName != null  && existingRoutineWithName.Id != id)
            return new RoutineResponse($"Routine with name {model.Name} already exists.");

        existingRoutine.Name = model.Name;
        existingRoutine.Date = model.Date;
        existingRoutine.State = model.State;

        try
        {
            _routineRepository.Update(existingRoutine);
            await _unitOfWork.Complete();
            return new RoutineResponse(model);
        }
        catch (Exception e)
        {
            return new RoutineResponse($"An exception occurred while saving routine: {e.Message}");
        }

    }

    public async Task<RoutineResponse> Delete(long id)
    {
        var existingRoutine = await _routineRepository.FindById(id);
        if (existingRoutine == null)
            return new RoutineResponse("Routine doesn't exist.");

        try
        {
            _routineRepository.Remove(existingRoutine);
            await _unitOfWork.Complete();
            return new RoutineResponse(existingRoutine);
        }
        catch (Exception e)
        {
            return new RoutineResponse($"An exception occurred while deleting routine: {e.Message}");
        }
    }
}