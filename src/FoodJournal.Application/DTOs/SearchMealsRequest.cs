using FoodJournal.Application.Enums;

namespace FoodJournal.Application.DTOs;

public sealed record SearchMealsRequest(
    DateTime? StartDate,
    DateTime? EndDate,
    Guid? FoodId,
    MealType? MealType,
    int PageNumber = 1,
    int PageSize = 10);
