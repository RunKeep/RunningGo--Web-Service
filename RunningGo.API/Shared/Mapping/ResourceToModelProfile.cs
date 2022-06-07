using AutoMapper;
using RunningGo.API.Shared.Domain.Models;
using RunningGo.API.Shared.Resources;

namespace RunningGo.API.Shared.Mapping;

public class ResourceToModelProfile: Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveUserResource, User>();
    }
}