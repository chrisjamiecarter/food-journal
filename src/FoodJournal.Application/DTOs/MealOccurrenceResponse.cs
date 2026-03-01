namespace FoodJournal.Application.DTOs;

public sealed record MealOccurrenceResponse(
    DateTime MealDate,
    string MealType);
