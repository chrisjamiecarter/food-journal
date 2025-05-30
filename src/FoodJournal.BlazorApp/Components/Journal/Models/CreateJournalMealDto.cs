using System.Collections.ObjectModel;
using FoodJournal.Application.Entities;
using FoodJournal.Application.Enums;

namespace FoodJournal.BlazorApp.Components.Journal.Models;

public class CreateJournalMealDto
{
    public Guid? Id { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public MealType Type { get; set; } = MealType.Breakfast;
    public Collection<Food> Foods { get; } = [];
}
