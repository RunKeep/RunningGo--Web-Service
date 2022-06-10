using RunningGo.API.Shared.Domain.Models;
using RunningGo.API.Shared.Domain.Repositories;
using RunningGo.API.Shared.Domain.Services;
using RunningGo.API.Shared.Domain.Services.Communication;

namespace RunningGo.API.Shared.Services;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    
    
    public async Task<IEnumerable<User>> List()
    {
        return await _userRepository.List();
    }
    

    public async Task<UserResponse> Save(User model)
    {
        try
        {
            await _userRepository.Add(model);
            await _unitOfWork.Complete();

            return new UserResponse(model);
        }
        catch (Exception e)
        {
            return new UserResponse($"An exception occurred while registrating user: {e.Message}");
        }
    }

    public async Task<UserResponse> Update(long id, User model)
    {
        var existingUser = await _userRepository.FindById(id);
        if (existingUser == null)
            return new UserResponse("User doesn't exist. Please register it.");

        existingUser.Name = model.Name;
        existingUser.LastName = model.LastName;
        existingUser.Age = model.Age;
        existingUser.Height = model.Height;
        existingUser.Weight = model.Weight;
        existingUser.Address = model.Address;
        existingUser.PhoneNumber = model.PhoneNumber;
        
        try
        {
            _userRepository.Update(existingUser);
            await _unitOfWork.Complete();

            return new UserResponse(existingUser);
        }
        catch (Exception e)
        {
            return new UserResponse($"An exception ocurred while updating user: {e.Message}");
        }
    }

    public async Task<UserResponse> Delete(long id)
    {
        var existingUser = await _userRepository.FindById(id);
        if (existingUser == null)
            return new UserResponse("User doesn't exist.");
        
        try
        {
            _userRepository.Remove(existingUser);
            await _unitOfWork.Complete();
            return new UserResponse(existingUser);
        }
        catch (Exception e)
        {
            return new UserResponse($"An error occurred while deleting user: {e.Message}");
        }
    }

    public async Task<UserResponse> FindById(long id)
    {
        var user = await _userRepository.FindById(id);
        if (user == null)
            return new UserResponse("User doesn't exist.");
        return new UserResponse(user);
    }
}