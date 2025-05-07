using FoodJournal.Application.Database;
using FoodJournal.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodJournal.Application.Repositories;

internal class MealRepository(ApplicationDbContext dbContext) : IMealRepository
{
    public async Task<bool> CreateAsync(Meal meal, CancellationToken cancellationToken)
    {
        await dbContext.Meals.AddAsync(meal, cancellationToken);
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(Meal meal, CancellationToken cancellationToken)
    {
        dbContext.Meals.Remove(meal);
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<List<Meal>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Meals.OrderBy(p => p.Date).ToListAsync(cancellationToken);
    }

    public async Task<Meal?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Meals.FindAsync([id], cancellationToken);
    }

    public async Task<bool> UpdateAsync(Meal meal, CancellationToken cancellationToken)
    {
        dbContext.Meals.Update(meal);
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }
}
