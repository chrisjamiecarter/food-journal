using System.Collections.ObjectModel;
using FoodJournal.Application.Entities;

namespace FoodJournal.BlazorApp.Components.Meals.Models;

public class CreateMealDto
{
    public string Name { get; set; } = string.Empty;
    public Collection<Food> Foods { get; } = [];
}
