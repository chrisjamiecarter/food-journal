using FoodJournal.Application.Entities;

namespace FoodJournal.Application.DTOs;

public sealed record MostFrequentFoodResponse(
    Food Food,
    int OccurrenceCount);
