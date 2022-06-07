using RunningGo.API.Shared.Domain.Models;

namespace RunningGo.API.Shared.Domain.Services.Communication;

public class UserResponse: BaseResponse<User>
{
    public UserResponse(string message): base(message) { }
    public UserResponse(User resource): base(resource) { }
}