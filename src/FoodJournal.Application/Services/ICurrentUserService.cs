namespace FoodJournal.Application.Services;

/// <summary>
/// Defines the service for identifying the current authenticated user.
/// </summary>
public interface ICurrentUserService
{
    string? Id { get; }
    string? Email { get; }
    string? Name { get; }
}
