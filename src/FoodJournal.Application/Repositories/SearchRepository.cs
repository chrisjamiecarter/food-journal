using FoodJournal.Application.Database;
using FoodJournal.Application.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FoodJournal.Application.Repositories;

public interface ISearchRepository
{
    Task<List<AvailableFoodResponse>> GetAvailableFoodsAsync(
        CancellationToken cancellationToken);

    Task<SearchMealsResponse> SearchMealsAsync(
        string userId,
        SearchMealsRequest request,
        CancellationToken cancellationToken);
}

internal sealed class SearchRepository(ApplicationDbContext dbContext) : ISearchRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<List<AvailableFoodResponse>> GetAvailableFoodsAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Foods
            .OrderBy(food => food.Name)
            .Select(food => new AvailableFoodResponse(
                food.Id,
                food.Name))
            .ToListAsync(cancellationToken);
    }

    public async Task<SearchMealsResponse> SearchMealsAsync(
        string userId,
        SearchMealsRequest request,
        CancellationToken cancellationToken)
    {
        var query = _dbContext.Meals
            .Include(meal => meal.Foods)
            .Where(meal => meal.UserId == userId);

        if (request.StartDate.HasValue)
        {
            query = query.Where(meal => meal.Date >= request.StartDate.Value.Date);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(meal => meal.Date <= request.EndDate.Value.Date);
        }

        if (request.MealType.HasValue)
        {
            query = query.Where(meal => meal.Type == request.MealType.Value);
        }

        if (request.FoodId.HasValue)
        {
            query = query.Where(meal => meal.Foods.Any(food => food.Id == request.FoodId.Value));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var meals = await query
            .OrderByDescending(meal => meal.Date)
            .ThenBy(meal => meal.Type)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new SearchMealsResponse(
            [.. meals.Select(meal => new SearchMealResponse(
                meal.Id,
                meal.Date,
                meal.Type,
                [.. meal.Foods.Select(food => food.Name)],
                meal.Foods.Count,
                meal.Foods.Sum(food => food.Calories)))],
            totalCount,
            request.PageNumber,
            request.PageSize);
    }
}
