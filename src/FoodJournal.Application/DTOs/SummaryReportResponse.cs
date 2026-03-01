namespace FoodJournal.Application.DTOs;

public sealed record SummaryReportResponse(
    int CaloriesToday,
    int CaloriesThisWeek,
    int CaloriesThisMonth,
    int AverageDailyCalories,
    MostFrequentFoodResponse? MostFrequentFoodThisMonth,
    MostFrequentFoodResponse? MostFrequentFoodAllTime);
