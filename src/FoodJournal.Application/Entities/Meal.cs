using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using FoodJournal.Application.Enums;
using FoodJournal.Application.Primitives;

namespace FoodJournal.Application.Entities;

/// <summary>
/// Represents a meal entity.
/// </summary>
public sealed class Meal : AggregateRoot
{
    [SetsRequiredMembers]
    public Meal(Guid id, DateTime date, MealType type) : base(id)
    {
        Date = date;
        Type = type;
    }

    public required DateTime Date { get; set; }
    public required MealType Type { get; set; }
    public required string UserId { get; set; }
    public ApplicationUser? User { get; set; }
    public Collection<Food> Foods { get; } = [];
}
