using FoodJournal.Application.Entities;
using FoodJournal.Application.Repositories;

namespace FoodJournal.Application.Services;

internal class FoodService(IFoodRepository foodRepository) : IFoodService
{
    public async Task<bool> CreateAsync(Food food, CancellationToken cancellationToken)
    {
        return await foodRepository.CreateAsync(food, cancellationToken);
    }

    public async Task<bool> DeleteAsync(Food food, CancellationToken cancellationToken)
    {
        return await foodRepository.DeleteAsync(food, cancellationToken);
    }

    public async Task<List<Food>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await foodRepository.GetAllAsync(cancellationToken);
    }

    public async Task<Food?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await foodRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<bool> UpdateAsync(Food food, CancellationToken cancellationToken)
    {
        return await foodRepository.UpdateAsync(food, cancellationToken);
    }
}
