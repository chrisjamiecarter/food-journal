namespace FoodJournal.Application.DTOs;

public record PagedResponse(
    int TotalCount,
    int PageNumber,
    int PageSize)
{
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
