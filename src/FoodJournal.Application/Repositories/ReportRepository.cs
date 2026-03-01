using FoodJournal.Application.Database;
using FoodJournal.Application.DTOs;
using FoodJournal.Application.Entities;
using FoodJournal.Application.Enums;
using Microsoft.EntityFrameworkCore;

namespace FoodJournal.Application.Repositories;

public interface IReportRepository
{
    Task<IReadOnlyCollection<FoodFrequencyResponse>> GetAllFoodsWithFrequencyAsync(
        string userId,
        DateTime startDate,
        DateTime endDate,
        CancellationToken cancellationToken);

    Task<FoodFrequencyReportResponse> GetFoodFrequencyReportAsync(
        string userId,
        FoodFrequencyRequest request,
        CancellationToken cancellationToken);

    Task<SummaryReportResponse> GetSummaryReportAsync(
        string userId,
        CancellationToken cancellationToken);
}

internal sealed class ReportRepository(ApplicationDbContext dbContext) : IReportRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<IReadOnlyCollection<FoodFrequencyResponse>> GetAllFoodsWithFrequencyAsync(string userId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
        var meals = await _dbContext.Meals
            .Include(meal => meal.Foods)
            .Where(meal => meal.UserId == userId && meal.Date >= startDate && meal.Date <= endDate)
            .ToListAsync(cancellationToken);

        return [.. meals
            .SelectMany(meal => meal.Foods.Select(food => new
            {
                Food = food,
                Meal = meal,
            }))
            .GroupBy(x => x.Food.Id)
            .Select(group => new FoodFrequencyResponse(
                group.Key,
                group.First().Food.Name,
                group.Count(),
                group.Sum(x => x.Food.Calories),
                group.Sum(x => x.Food.Carbs),
                group.Sum(x => x.Food.Protein),
                group.Sum(x => x.Food.Fat),
                [.. group.Select(x => new MealOccurrenceResponse(
                    x.Meal.Date,
                    x.Meal.Type.ToString()))
                .OrderByDescending(order => order.MealDate)]))];
    }

    public async Task<FoodFrequencyReportResponse> GetFoodFrequencyReportAsync(string userId, FoodFrequencyRequest request, CancellationToken cancellationToken)
    {
        var dateRange = GetDateRange(request.Period, request.StartDate, request.EndDate);

        var results = await GetAllFoodsWithFrequencyAsync(userId, dateRange.StartDate, dateRange.EndDate, cancellationToken);

        if (request.FoodId.HasValue)
        {
            results = [.. results.Where(food => food.FoodId == request.FoodId.Value)];
        }
        else if (!string.IsNullOrEmpty(request.FoodName))
        {
            results = [.. results.Where(food => food.FoodName.Contains(request.FoodName, StringComparison.OrdinalIgnoreCase))];
        }

        return new FoodFrequencyReportResponse(
            [.. results.OrderByDescending(food => food.OccurrenceCount)],
            results.Count,
            dateRange.StartDate,
            dateRange.EndDate,
            request.Period);
    }

    public async Task<SummaryReportResponse> GetSummaryReportAsync(string userId, CancellationToken cancellationToken)
    {
        var today = DateTime.Today;
        var startOfWeek = GetStartOfWeek(today);
        var startOfMonth = new DateTime(today.Year, today.Month, 1);

        var allMealsWithFoods = await _dbContext.Meals
            .Include(meal => meal.Foods)
            .Where(meal => meal.UserId == userId)
            .ToListAsync(cancellationToken);

        var todaysMeals = allMealsWithFoods.Where(meal => meal.Date.Date == today);
        var todaysCalories = todaysMeals.SelectMany(meal => meal.Foods).Sum(food => food.Calories);

        var weeksMeals = allMealsWithFoods.Where(meal => meal.Date.Date >= startOfWeek);
        var weeksCalories = weeksMeals.SelectMany(meal => meal.Foods).Sum(food => food.Calories);

        var monthsMeals = allMealsWithFoods.Where(meal => meal.Date.Date >= startOfMonth);
        var monthsCalories = monthsMeals.SelectMany(meal => meal.Foods).Sum(food => food.Calories);

        var averageDailyCalories = monthsCalories / today.Day;

        var monthsMostFrequentFood = GetMostFrequentFood(monthsMeals);

        var allTimeMostFrequentFood = GetMostFrequentFood(allMealsWithFoods);

        return new SummaryReportResponse(
            todaysCalories,
            weeksCalories,
            monthsCalories,
            averageDailyCalories,
            monthsMostFrequentFood,
            allTimeMostFrequentFood);
    }

    private sealed record DateRange(
        DateTime StartDate,
        DateTime EndDate);

    private static DateRange GetDateRange(
        ReportPeriod period,
        DateTime? customStart,
        DateTime? customEnd)
    {
        var today = DateTime.Today;
        var endDate = customEnd ?? today;

        var startDate = period switch
        {
            ReportPeriod.Week => GetStartOfWeek(today),
            ReportPeriod.Month => new DateTime(today.Year, today.Month, 1),
            ReportPeriod.Year => new DateTime(today.Year, 1, 1),
            ReportPeriod.AllTime => DateTime.MinValue,
            _ => customStart ?? new DateTime(today.Year, today.Month, 1)
        };

        if (customStart.HasValue && period == ReportPeriod.Month)
        {
            startDate = customStart.Value;
        }

        return new DateRange(startDate, endDate);
    }

    private static MostFrequentFoodResponse? GetMostFrequentFood(IEnumerable<Meal> meals)
    {
        return meals
            .SelectMany(meal => meal.Foods)
            .GroupBy(food => food.Id)
            .Select(group => new MostFrequentFoodResponse(
                group.First().Name,
                group.Count()))
            .OrderByDescending(food => food.OccurrenceCount)
            .FirstOrDefault();
    }

    private static DateTime GetStartOfWeek(DateTime today)
    {
        var startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
        if (today.DayOfWeek == DayOfWeek.Sunday)
        {
            startOfWeek = startOfWeek.AddDays(-7);
        }

        return startOfWeek;
    }
}
