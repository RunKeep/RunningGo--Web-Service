using AutoMapper;
using RunningGo.API.Checkeos.Domain.Models;
using RunningGo.API.Checkeos.Resources;
using RunningGo.API.Dietas.Domain.Models;
using RunningGo.API.Dietas.Resources;
using RunningGo.API.Rutinas.Domain.Models;
using RunningGo.API.Rutinas.Resources;
using RunningGo.API.Shared.Domain.Models;
using RunningGo.API.Shared.Resources;
using RunningGo.API.SistemaDeMetas.Domain.Models;
using RunningGo.API.SistemaDeMetas.Resources;

namespace RunningGo.API.Shared.Mapping;

public class ResourceToModelProfile: Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveUserResource, User>();
        
        CreateMap<SaveFoodResource, Food>();
        CreateMap<SaveDietResource, Diet>();

        CreateMap<SaveHabitResource, Habit>();
        CreateMap<SaveRoutineResource, Routine>();

        CreateMap<SaveGoalResource, Goal>();
        CreateMap<SaveProcessResource, Process>();

        CreateMap<SaveSpecialistResource, Specialist>();
        CreateMap<SaveCheckupResource, Checkup>();
    }
}