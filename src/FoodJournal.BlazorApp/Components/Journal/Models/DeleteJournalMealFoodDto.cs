using FoodJournal.Application.Enums;

namespace FoodJournal.BlazorApp.Components.Journal.Models;

public class DeleteJournalMealFoodDto
{
    public DateTime Date { get; set; } = DateTime.Now;
    public MealType Type { get; set; } = MealType.Breakfast;
    public Guid FoodId { get; set; }
}
