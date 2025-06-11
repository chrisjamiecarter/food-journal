using FoodJournal.Application.Entities;
using FoodJournal.Application.Enums;
using FoodJournal.Application.Repositories;

namespace FoodJournal.Application.Services;

internal sealed class MealService(IMealRepository mealRepository) : IMealService
{
    public async Task<bool> CreateAsync(Meal meal, CancellationToken cancellationToken)
    {
        // Set the meal date to the date only.
        meal.Date = meal.Date.Date;

        return await mealRepository.CreateAsync(meal, cancellationToken);
    }

    public async Task<bool> DeleteAsync(Meal meal, CancellationToken cancellationToken)
    {
        return await mealRepository.DeleteAsync(meal, cancellationToken);
    }

    public async Task<Meal?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await mealRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<List<Meal>> GetByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        return await mealRepository.GetByUserIdAsync(userId, cancellationToken);
    }

    public async Task<Meal?> GetByUserIdAndDateAndTypeAsync(string userId, DateTime mealDate, MealType mealType, CancellationToken cancellationToken)
    {
        // Set the meal date to the date only.
        mealDate = mealDate.Date;

        return await mealRepository.GetByUserIdAndDateAndTypeAsync(userId, mealDate, mealType, cancellationToken);
    }

    public async Task<bool> UpdateAsync(Meal meal, CancellationToken cancellationToken)
    {
        // Set the meal date to the date only.
        meal.Date = meal.Date.Date;

        return await mealRepository.UpdateAsync(meal, cancellationToken);
    }
}
