using FoodJournal.Application.Entities;
using FoodJournal.Application.Enums;

namespace FoodJournal.BlazorApp.Components.Meals.Models;

public class CreateMealDto
{
    public DateTime Date { get; set; } = DateTime.Now;
    public MealType Type { get; set; } = MealType.Breakfast;
    public List<Food> Foods { get; set; } = [];
}
