using FoodJournal.Application.Database;
using FoodJournal.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodJournal.Application.Repositories;

internal sealed class QuickMealRepository(ApplicationDbContext dbContext) : IQuickMealRepository
{
    public async Task<bool> CreateAsync(QuickMeal quickMeal, CancellationToken cancellationToken)
    {
        await dbContext.QuickMeals.AddAsync(quickMeal, cancellationToken);
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(QuickMeal quickMeal, CancellationToken cancellationToken)
    {
        dbContext.QuickMeals.Remove(quickMeal);
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<QuickMeal?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.QuickMeals.FindAsync([id], cancellationToken);
    }

    public async Task<List<QuickMeal>> GetByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        var query = dbContext.QuickMeals.Include(x => x.Foods).AsQueryable();

        query = query.Where(x => x.UserId == userId);

        query = query.OrderBy(p => p.Id);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<bool> UpdateAsync(QuickMeal quickMeal, CancellationToken cancellationToken)
    {
        dbContext.QuickMeals.Update(quickMeal);
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }
}
