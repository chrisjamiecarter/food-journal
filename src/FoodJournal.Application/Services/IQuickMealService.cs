using FoodJournal.Application.Entities;
using FoodJournal.Application.Enums;

namespace FoodJournal.Application.Services;

public interface IQuickMealService
{
    Task<bool> CreateAsync(QuickMeal quickMeal, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(QuickMeal quickMeal, CancellationToken cancellationToken);
    Task<List<QuickMeal>> GetByUserIdAsync(string userId, CancellationToken cancellationToken);
    Task<QuickMeal?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(QuickMeal quickMeal, CancellationToken cancellationToken);
}