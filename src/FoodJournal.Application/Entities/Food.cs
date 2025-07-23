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
    public Food(Guid id,
                string name,
                string base64Image,
                string servingSize,
                int calories,
                float carbs,
                float protein,
                float fat) : base(id)
    {
        Name = name;
        Base64Image = base64Image;
        ServingSize = servingSize;
        Calories = calories;
        Carbs = carbs;
        Protein = protein;
        Fat = fat;
    }

    public required string Name { get; set; }
    public required string Base64Image { get; set; }
    public required string ServingSize { get; set; }
    public required int Calories { get; set; }
    public required float Carbs { get; set; }
    public required float Protein { get; set; }
    public required float Fat { get; set; }
    public Collection<Meal> Meals { get; } = [];
    public Collection<QuickMeal> QuickMeals { get; } = [];
}
