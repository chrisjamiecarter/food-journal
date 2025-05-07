using FoodJournal.Application.Entities;

namespace FoodJournal.Application.Services;

public interface IMealService
{
    Task<bool> CreateAsync(Meal meal, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Meal meal, CancellationToken cancellationToken);
    Task<List<Meal>> GetAllAsync(CancellationToken cancellationToken);
    Task<Meal?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Meal meal, CancellationToken cancellationToken);
}