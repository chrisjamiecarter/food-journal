namespace FoodJournal.Application.DTOs;

public sealed record MostFrequentFoodResponse(
    string FoodName,
    int OccurrenceCount);
