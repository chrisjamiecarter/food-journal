using FoodJournal.Application.Database;
using FoodJournal.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodJournal.Application.Repositories;

internal sealed class FoodRepository(ApplicationDbContext dbContext) : IFoodRepository
{
    public async Task<bool> CreateAsync(Food food, CancellationToken cancellationToken)
    {
        await dbContext.Foods.AddAsync(food, cancellationToken);
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(Food food, CancellationToken cancellationToken)
    {
        dbContext.Foods.Remove(food);
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<List<Food>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Foods.OrderBy(p => p.Name).ToListAsync(cancellationToken);
    }

    public async Task<Food?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Foods.FindAsync([id], cancellationToken);
    }

    public async Task<bool> UpdateAsync(Food food, CancellationToken cancellationToken)
    {
        dbContext.Foods.Update(food);
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }
}
