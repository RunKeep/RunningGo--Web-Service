using RunningGo.API.Dietas.Domain.Models;

namespace RunningGo.API.Dietas.Domain.Repositories;

public interface IFoodRepository
{
    Task<IEnumerable<Food>> List();
    Task Add(Food food);
    Task<Food> FindById(int id);
    Task<Food> FindByName(string name);
    void Update(Food food);
    void Remove(Food food);
}