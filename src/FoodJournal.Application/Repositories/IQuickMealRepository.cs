using FoodJournal.Application.Entities;

namespace FoodJournal.Application.Repositories;

public interface IQuickMealRepository
{
    Task<bool> CreateAsync(QuickMeal meal, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(QuickMeal meal, CancellationToken cancellationToken);
    Task<List<QuickMeal>> GetAllAsync(CancellationToken cancellationToken);
    Task<QuickMeal?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<QuickMeal>> GetByUserIdAsync(string userId, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(QuickMeal meal, CancellationToken cancellationToken);
}
