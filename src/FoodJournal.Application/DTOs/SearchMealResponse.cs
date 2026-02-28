using FoodJournal.Application.Enums;

namespace FoodJournal.Application.DTOs;

public sealed record SearchMealResponse(
    Guid MealId,
    DateTime MealDate,
    MealType? MealType,
    IReadOnlyCollection<string> FoodNames,
    int TotalFoods,
    int TotalCalories);
