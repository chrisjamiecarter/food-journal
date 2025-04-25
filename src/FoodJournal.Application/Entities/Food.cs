using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using FoodJournal.Application.Primitives;

namespace FoodJournal.Application.Entities;

/// <summary>
/// Represents a food entity.
/// </summary>
public sealed class Food : AggregateRoot
{
    [SetsRequiredMembers]
    public Food(Guid id, string name) : base(id)
    {
        Name = name;
    }

    public required string Name { get; set; }
    public Collection<Meal> Meals { get; } = [];
}
