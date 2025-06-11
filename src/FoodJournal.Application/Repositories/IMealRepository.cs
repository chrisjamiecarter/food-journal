using FoodJournal.Application.Entities;
using FoodJournal.Application.Enums;

namespace FoodJournal.Application.Repositories;

public interface IMealRepository
{
    Task<bool> CreateAsync(Meal meal, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Meal meal, CancellationToken cancellationToken);
    Task<List<Meal>> GetAllAsync(CancellationToken cancellationToken);
    Task<Meal?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Meal?> GetByUserIdAndDateAndTypeAsync(string userId, DateTime mealDate, MealType mealType, CancellationToken cancellationToken);
    Task<List<Meal>> GetByUserIdAsync(string userId, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Meal meal, CancellationToken cancellationToken);
}
