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
        var existingUserWithEmail = await _userRepository.FindByEmail(model.Email);
        if (existingUserWithEmail != null)
            return new UserResponse("An user with this email already exists.");
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
        
        var existingUserWithEmail = await _userRepository.FindByEmail(model.Email);
        if (existingUserWithEmail != null && existingUserWithEmail.Id != id)
            return new UserResponse("An user with this email already exists.");

        existingUser.Name = model.Name;
        existingUser.LastName = model.LastName;
        existingUser.Email = model.Email;
        existingUser.Password = model.Password;
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
            return new UserResponse($"An exception occurred while updating user: {e.Message}");
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

    public async Task<UserResponse> SignIn(User user)
    {
        var existingUser = await _userRepository.FindByEmailAndPassword(user.Email, user.Password);
        if (existingUser == null)
            return new UserResponse("Incorrect email or password. Try again.");

        return new UserResponse(existingUser, DateTime.Now);
    }
}