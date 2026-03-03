using FoodJournal.Application.Entities;
using FoodJournal.Application.Repositories;

namespace FoodJournal.Application.Services;

public interface IQuickMealService
{
    Task<bool> CreateAsync(QuickMeal quickMeal, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(QuickMeal quickMeal, CancellationToken cancellationToken);
    Task<List<QuickMeal>> GetByUserIdAsync(string userId, CancellationToken cancellationToken);
    Task<QuickMeal?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(QuickMeal quickMeal, CancellationToken cancellationToken);
}

internal sealed class QuickMealService(IQuickMealRepository quickMealRepository) : IQuickMealService
{
    public async Task<bool> CreateAsync(QuickMeal quickMeal, CancellationToken cancellationToken)
    {
        return await quickMealRepository.CreateAsync(quickMeal, cancellationToken);
    }

    public async Task<bool> DeleteAsync(QuickMeal quickMeal, CancellationToken cancellationToken)
    {
        return await quickMealRepository.DeleteAsync(quickMeal, cancellationToken);
    }

    public async Task<List<QuickMeal>> GetByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        return await quickMealRepository.GetByUserIdAsync(userId, cancellationToken);
    }

    public async Task<QuickMeal?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await quickMealRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<bool> UpdateAsync(QuickMeal quickMeal, CancellationToken cancellationToken)
    {
        return await quickMealRepository.UpdateAsync(quickMeal, cancellationToken);
    }
}
