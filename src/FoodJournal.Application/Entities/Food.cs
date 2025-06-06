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
    public Food(Guid id, string name, string base64Image) : base(id)
    {
        Name = name;
        Base64Image = base64Image;
    }

    public required string Name { get; set; }
    public required string Base64Image { get; set; }
    public Collection<Meal> Meals { get; } = [];
}
