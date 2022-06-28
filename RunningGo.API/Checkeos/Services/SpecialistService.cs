using RunningGo.API.Checkeos.Domain.Models;
using RunningGo.API.Checkeos.Domain.Repositories;
using RunningGo.API.Checkeos.Domain.Services;
using RunningGo.API.Checkeos.Domain.Services.Communication;
using RunningGo.API.Shared.Domain.Repositories;

namespace RunningGo.API.Checkeos.Services;

public class SpecialistService: ISpecialistService
{
    private readonly ISpecialistRepository _specialistRepository;
    private IUnitOfWork _unitOfWork;

    public SpecialistService(ISpecialistRepository specialistRepository, IUnitOfWork unitOfWork)
    {
        _specialistRepository = specialistRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Specialist>> List()
    {
        return await _specialistRepository.List();
    }

    public async Task<SpecialistResponse> Save(Specialist model)
    {
        var existingSpecialistWithName = await _specialistRepository.FindByName(model.Name);
        if (existingSpecialistWithName != null)
            return new SpecialistResponse("A specialist with same name already exists.");

        try
        {
            await _specialistRepository.Add(model);
            await _unitOfWork.Complete();
            return new SpecialistResponse(model);
        }
        catch (Exception e)
        {
            return new SpecialistResponse($"An exception occurred while saving specialist: {e.Message}");
        }
    }

    public async Task<SpecialistResponse> Update(long id, Specialist model)
    {
        var existingSpecialist = await _specialistRepository.FindById(id);
        if (existingSpecialist == null)
            return new SpecialistResponse("Specialist doesn't exist. Please create it.");

        var existingSpecialistWithName = await _specialistRepository.FindByName(model.Name);
        if (existingSpecialistWithName != null && existingSpecialistWithName.Id != id)
            return new SpecialistResponse("A specialist with same name already exists.");


        existingSpecialist.Name = model.Name;
        existingSpecialist.Degree = model.Degree;

        try
        {
            _specialistRepository.Update(existingSpecialist);
            await _unitOfWork.Complete();
            return new SpecialistResponse(existingSpecialist);
        }
        catch (Exception e)
        {
            return new SpecialistResponse($"An exception occurred while updating specialist: {e.Message}");
        }
    }

    public async Task<SpecialistResponse> Delete(long id)
    {
        var existingSpecialist = await _specialistRepository.FindById(id);
        if (existingSpecialist == null)
            return new SpecialistResponse("Specialist doesn't exist");
        try
        {
            _specialistRepository.Remove(existingSpecialist);
            await _unitOfWork.Complete();
            return new SpecialistResponse(existingSpecialist);
        }
        catch (Exception e)
        {
            return new SpecialistResponse($"An exception occurred while deleting specialist: {e.Message}");
        }
    }

    public async Task<SpecialistResponse> FindById(long id)
    {
        var specialist = await _specialistRepository.FindById(id);
        if (specialist == null)
            return new SpecialistResponse("Specialist doesn't exist.");
        return new SpecialistResponse(specialist);
    }
}