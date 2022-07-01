using RunningGo.API.Shared.Domain.Models;
using RunningGo.API.Shared.Domain.Services.Communication;

namespace RunningGo.API.Shared.Domain.Services;

public interface IUserService: IBaseService<User, UserResponse, long>
{
    Task<UserResponse> FindById(long id);
    Task<UserResponse> SignIn(User user);
}