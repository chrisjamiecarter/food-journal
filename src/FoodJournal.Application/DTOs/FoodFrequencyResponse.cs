namespace FoodJournal.Application.DTOs;

public sealed record FoodFrequencyResponse(
    Guid FoodId,
    string FoodName,
    int OccurrenceCount,
    int TotalCalories,
    float TotalCarbs,
    float TotalProtein,
    float TotalFat,
    IReadOnlyCollection<MealOccurrenceResponse> Occurrences);
