using RunningGo.API.Rutinas.Domain.Models;
using RunningGo.API.Rutinas.Domain.Services.Communication;
using RunningGo.API.Shared.Domain.Services;

namespace RunningGo.API.Rutinas.Domain.Services;

public interface IHabitService: IBaseService<Habit, HabitResponse, int>
{
    Task<HabitResponse> FindById(int id);
}