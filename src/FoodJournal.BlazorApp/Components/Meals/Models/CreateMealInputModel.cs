using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using FoodJournal.Application.Entities;

namespace FoodJournal.BlazorApp.Components.Meals.Models;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Maintainability", "CA1515:Consider making public types internal", Justification = "Component parameters should be public.")]
public sealed class CreateMealInputModel
{
    [Required(ErrorMessage = "Please enter a meal name.")]
    [DataType(DataType.Text)]
    public string Name { get; set; } = string.Empty;

    [MinLength(1, ErrorMessage = "Please select at least one food item.")]
    public Collection<Food> Foods { get; } = [];
}
