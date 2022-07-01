using RunningGo.API.Checkeos.Domain.Models;
using RunningGo.API.Checkeos.Domain.Repositories;
using RunningGo.API.Checkeos.Domain.Services;
using RunningGo.API.Checkeos.Domain.Services.Communication;
using RunningGo.API.Shared.Domain.Repositories;

namespace RunningGo.API.Checkeos.Services;

public class ArrangeService: IArrangeService
{
    private readonly IArrangeRepository _arrangeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ArrangeService(IArrangeRepository arrangeRepository, IUnitOfWork unitOfWork)
    {
        _arrangeRepository = arrangeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Arrange>> List()
    {
        return await _arrangeRepository.List();
    }

    public async Task<ArrangeResponse> Save(Arrange model)
    {
        try
        {
            await _arrangeRepository.Add(model);
            await _unitOfWork.Complete();
            return new ArrangeResponse(model);
        }
        catch (Exception e)
        {
            return new ArrangeResponse($"An exception occurred while saving arrange: {e.Message}");
        }
    }

    public async Task<ArrangeResponse> Update(int id, Arrange model)
    {
        var existingArrange = await _arrangeRepository.FindById(id);
        if (existingArrange == null)
            return new ArrangeResponse("Arrange doesn't exist. Please create it.");

        existingArrange.State = model.State;
        
        try
        {
            _arrangeRepository.Update(existingArrange);
            await _unitOfWork.Complete();
            return new ArrangeResponse(existingArrange);
        }
        catch (Exception e)
        {
            return new ArrangeResponse($"An exception occurred while updating arrange: {e.Message}");
        }
    }

    public async Task<ArrangeResponse> Delete(int id)
    {
        var existingArrange = await _arrangeRepository.FindById(id);
        if (existingArrange == null)
            return new ArrangeResponse("Arrange doesn't exist.");

        try
        {
            _arrangeRepository.Remove(existingArrange);
            await _unitOfWork.Complete();
            return new ArrangeResponse(existingArrange);
        }
        catch (Exception e)
        {
            return new ArrangeResponse($"An exception occurred while deleting arrange: {e.Message}");
        }
    }

    public async Task<ArrangeResponse> FindById(int id)
    {
        var arrange = await _arrangeRepository.FindById(id);
        if (arrange == null)
            return new ArrangeResponse("Arrange doesn't exist.");
        return new ArrangeResponse(arrange);
    }
}