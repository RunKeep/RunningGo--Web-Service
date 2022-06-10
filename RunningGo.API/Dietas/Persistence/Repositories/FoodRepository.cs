using Microsoft.EntityFrameworkCore;
using RunningGo.API.Dietas.Domain.Models;
using RunningGo.API.Dietas.Domain.Repositories;
using RunningGo.API.Shared.Persistence.Contexts;
using RunningGo.API.Shared.Persistence.Repositories;

namespace RunningGo.API.Dietas.Persistence.Repositories;

public class FoodRepository: BaseRepository, IFoodRepository
{
    public FoodRepository(EnhancedDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Food>> List()
    {
        return await _context.Foods.ToListAsync();
    }

    public async Task Add(Food food)
    {
        await _context.Foods.AddAsync(food);
    }

    public async Task<Food> FindById(int id)
    {
        return await _context.Foods.FindAsync(id);
    }

    public async Task<Food> FindByName(string title)
    {
        return await _context.Foods.FirstOrDefaultAsync(p => p.Name == p.Name);
    }

    public void Update(Food food)
    {
        _context.Foods.Update(food);
    }

    public void Remove(Food food)
    {
        _context.Foods.Remove(food);
    }
}