using FoodJournal.Application.Entities;

namespace FoodJournal.Application.Services;

public interface IFoodService
{
    Task<bool> CreateAsync(Food food, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Food food, CancellationToken cancellationToken);
    Task<List<Food>> GetAllAsync(CancellationToken cancellationToken);
    Task<Food?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Food food, CancellationToken cancellationToken);
}