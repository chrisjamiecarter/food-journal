using FoodJournal.Application.Enums;

namespace FoodJournal.BlazorApp.Components.Journal.Models;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Maintainability", "CA1515:Consider making public types internal", Justification = "Component parameters should be public.")]
public sealed class DeleteJournalMealFoodDto
{
    public DateTime Date { get; set; } = DateTime.Now;
    public MealType Type { get; set; } = MealType.Breakfast;
    public Guid FoodId { get; set; }
}
