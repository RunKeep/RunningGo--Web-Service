using RunningGo.API.Checkeos.Domain.Models;
using RunningGo.API.Checkeos.Domain.Repositories;
using RunningGo.API.Checkeos.Domain.Services;
using RunningGo.API.Checkeos.Domain.Services.Communication;
using RunningGo.API.Shared.Domain.Repositories;

namespace RunningGo.API.Checkeos.Services;

public class CheckupService: ICheckupService
{
    private readonly ICheckupRepository _checkupRepository;
    private readonly IUserRepository _userRepository;
    private readonly ISpecialistRepository _specialistRepository;
    private readonly IArrangeRepository _arrangeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CheckupService(ICheckupRepository checkupRepository, IUserRepository userRepository, ISpecialistRepository specialistRepository, IUnitOfWork unitOfWork, IArrangeRepository arrangeRepository)
    {
        _checkupRepository = checkupRepository;
        _userRepository = userRepository;
        _specialistRepository = specialistRepository;
        _unitOfWork = unitOfWork;
        _arrangeRepository = arrangeRepository;
    }

    public async Task<IEnumerable<Checkup>> List()
    {
        return await _checkupRepository.List();
    }

    public async Task<CheckupResponse> Save(Checkup model)
    {
        var existingArrange = await _arrangeRepository.FindById(model.ArrangeId);
        if (existingArrange == null)
            return new CheckupResponse($"Arrange with id {model.ArrangeId} doesn't exist");

        var existingCheckupWithArrange = await _checkupRepository.FindByArrangeId(model.ArrangeId);
        if (existingCheckupWithArrange != null)
            return new CheckupResponse($"Arrange with id {model.ArrangeId} is used by other checkup.");
        
        var existingUser = await _userRepository.FindById(model.UserId);
        if (existingUser == null)
            return new CheckupResponse($"User with id {model.UserId} doesn't exist");
        
        var existingSpecialist = await _specialistRepository.FindById(model.SpecialistId);
        if (existingSpecialist == null)
            return new CheckupResponse($"Specialist with id {model.SpecialistId} doesn't exist");

        try
        {
            await _checkupRepository.Add(model);
            await _unitOfWork.Complete();
            return new CheckupResponse(model);
        }
        catch (Exception e)
        {
            return new CheckupResponse($"An exception occurred while saving checkup: {e.Message}");
        }
    }

    public async Task<CheckupResponse> Update(int id, Checkup model)
    {
        var existingCheckup = await _checkupRepository.FindById(id);
        if (existingCheckup == null)
            return new CheckupResponse("Checkup doesn't exist. Please create it.");
        
        var existingArrange = await _arrangeRepository.FindById(model.ArrangeId);
        if (existingArrange == null)
            return new CheckupResponse($"Arrange with id {model.ArrangeId} doesn't exist");
        
        var existingCheckupWithArrange = await _checkupRepository.FindByArrangeId(model.ArrangeId);
        if (existingCheckupWithArrange != null)
            return new CheckupResponse($"Arrange with id {model.ArrangeId} is used by other checkup.");

        var existingUser = await _userRepository.FindById(model.UserId);
        if (existingUser == null)
            return new CheckupResponse($"User with id {model.UserId} doesn't exist");

        var existingSpecialist = await _specialistRepository.FindById(model.SpecialistId);
        if (existingSpecialist == null)
            return new CheckupResponse($"Specialist with id {model.SpecialistId} doesn't exist");

        existingCheckup.Date = model.Date;
        existingCheckup.UserData = model.UserData;
        existingCheckup.Results = model.Results;

        try
        {
            _checkupRepository.Update(existingCheckup);
            await _unitOfWork.Complete();
            return new CheckupResponse(existingCheckup);
        }
        catch (Exception e)
        {
            return new CheckupResponse($"An exception occurred while updating checkup: {e.Message}");
        }
    }

    public async Task<CheckupResponse> Delete(int id)
    {
        var existingCheckup = await _checkupRepository.FindById(id);
        if (existingCheckup == null)
            return new CheckupResponse("Checkup doesn't exist.");

        try
        {
            _checkupRepository.Remove(existingCheckup);
            await _unitOfWork.Complete();
            return new CheckupResponse(existingCheckup);
        }
        catch (Exception e)
        {
            return new CheckupResponse($"An exception occurred while deleting checkup: {e.Message}");
        }
    }

    public async Task<CheckupResponse> FindById(int id)
    {
        var checkup = await _checkupRepository.FindById(id);
        if (checkup == null)
            return new CheckupResponse("Checkup doesn't exist.");
        return new CheckupResponse(checkup);
    }

    public async Task<IEnumerable<Checkup>> ListByUserId(long userId)
    {
        return await _checkupRepository.ListByUserId(userId);
    }

    public async Task<IEnumerable<Checkup>> ListBySpecialistId(long specialistId)
    {
        return await _checkupRepository.ListBySpecialistId(specialistId);
    }
}