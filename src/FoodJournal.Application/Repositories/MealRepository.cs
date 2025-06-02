using FoodJournal.Application.Database;
using FoodJournal.Application.Entities;
using FoodJournal.Application.Enums;
using Microsoft.EntityFrameworkCore;

namespace FoodJournal.Application.Repositories;

internal sealed class MealRepository(ApplicationDbContext dbContext) : IMealRepository
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

    public async Task<Meal?> GetByUserIdAndDateAndTypeAsync(string userId, DateTime mealDate, MealType mealType, CancellationToken cancellationToken)
    {
        var query = dbContext.Meals.Include(x => x.Foods).AsQueryable();

        query = query.Where(x => x.UserId == userId);

        query = query.Where(x => x.Date == mealDate);

        query = query.Where(x => x.Type == mealType);

        return await query.SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Meal>> GetByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        var query = dbContext.Meals.Include(x => x.Foods).AsQueryable();

        query = query.Where(x => x.UserId == userId);

        query = query.OrderBy(p => p.Date);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<bool> UpdateAsync(Meal meal, CancellationToken cancellationToken)
    {
        dbContext.Meals.Update(meal);
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }
}
