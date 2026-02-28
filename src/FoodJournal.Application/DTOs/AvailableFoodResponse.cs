namespace FoodJournal.Application.DTOs;

public sealed record AvailableFoodResponse(
    Guid FoodId,
    string FoodName);
