using AutoMapper;
using RunningGo.API.Dietas.Domain.Models;
using RunningGo.API.Dietas.Resources;
using RunningGo.API.Shared.Domain.Models;
using RunningGo.API.Shared.Resources;

namespace RunningGo.API.Shared.Mapping;

public class ModelToResourceProfile: Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<User, UserResource>();
        
        CreateMap<Food, FoodResource>();
    }
}