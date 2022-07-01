using RunningGo.API.Shared.Domain.Repositories;
using RunningGo.API.SistemaDeMetas.Domain.Models;
using RunningGo.API.SistemaDeMetas.Domain.Repositories;
using RunningGo.API.SistemaDeMetas.Domain.Services;
using RunningGo.API.SistemaDeMetas.Domain.Services.Communication;

namespace RunningGo.API.SistemaDeMetas.Services;

public class ProcessService: IProcessService
{
    private readonly IProcessRepository _processRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProcessService(IProcessRepository processRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _processRepository = processRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Process>> List()
    {
        return await _processRepository.List();
    }

    public async Task<ProcessResponse> Save(Process model)
    {
        var existingUser = await _userRepository.FindById(model.UserId);
        if (existingUser == null)
            return new ProcessResponse($"User with id {model.UserId} doesn't exist");

        var existingProcessWithDescription = await _processRepository.FindByDescription(model.Description);
        if (existingProcessWithDescription != null)
            if(existingProcessWithDescription.UserId == model.UserId)
                return new ProcessResponse($"A process with description {model.Description} already exists");

        try
        {
            await _processRepository.Add(model);
            await _unitOfWork.Complete();
            return new ProcessResponse(model);
        }
        catch (Exception e)
        {
            return new ProcessResponse($"An exception occurred while saving process: {e.Message}");
        }
    }

    public async Task<ProcessResponse> Update(int id, Process model)
    {
        var existingProcess = await _processRepository.FindById(id);
        if (existingProcess == null)
            return new ProcessResponse($"Process doesn't exist. Please create it.");
        

        var existingProcessWithDescription = await _processRepository.FindByDescription(model.Description);
        if (existingProcessWithDescription != null && existingProcessWithDescription.Id != id)
            if(existingProcessWithDescription.UserId == model.UserId )
                return new ProcessResponse($"A process with description {model.Description} already exists");

        existingProcess.Description = model.Description;
        existingProcess.State = model.State;
        existingProcess.Date = model.Date;
        
        try
        {
            _processRepository.Update(existingProcess);
            await _unitOfWork.Complete();
            return new ProcessResponse(existingProcess);
        }
        catch (Exception e)
        {
            return new ProcessResponse($"An exception occurred while updating process: {e.Message}");
        }
    }

    public async Task<ProcessResponse> Delete(int id)
    {
        var existingProcess = await _processRepository.FindById(id);
        if (existingProcess == null)
            return new ProcessResponse("Process doesn't exist");

        try
        {
            _processRepository.Remove(existingProcess);
            await _unitOfWork.Complete();
            return new ProcessResponse(existingProcess);
        }
        catch (Exception e)
        {
            return new ProcessResponse($"An exception occurred while updating process: {e.Message}");
        }
    }

    public async Task<ProcessResponse> FindById(int id)
    {
        var process = await _processRepository.FindById(id);
        if (process == null)
            return new ProcessResponse("Process doesn't exist");
        return new ProcessResponse(process);
    }

    public async Task<IEnumerable<Process>> ListByUserId(long userId)
    {
        return await _processRepository.ListByUserId(userId);
    }
}