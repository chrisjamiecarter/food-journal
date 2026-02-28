namespace FoodJournal.Application.DTOs;

public sealed record SearchMealsResponse(
    IReadOnlyCollection<SearchMealResponse> Meals,
    int TotalCount,
    int PageNumber,
    int PageSize) : PagedResponse(TotalCount, PageNumber, PageSize);
