using FoodJournal.Application.Entities;
using FoodJournal.Application.Repositories;

namespace FoodJournal.Application.Services;

internal class MealService(IMealRepository mealRepository) : IMealService
{
    public async Task<bool> CreateAsync(Meal meal, CancellationToken cancellationToken)
    {
        return await mealRepository.CreateAsync(meal, cancellationToken);
    }

    public async Task<bool> DeleteAsync(Meal meal, CancellationToken cancellationToken)
    {
        return await mealRepository.DeleteAsync(meal, cancellationToken);
    }

    public async Task<List<Meal>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await mealRepository.GetAllAsync(cancellationToken);
    }

    public async Task<Meal?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await mealRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<bool> UpdateAsync(Meal meal, CancellationToken cancellationToken)
    {
        return await mealRepository.UpdateAsync(meal, cancellationToken);
    }
}
