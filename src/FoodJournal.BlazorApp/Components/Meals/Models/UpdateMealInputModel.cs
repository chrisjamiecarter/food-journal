using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using FoodJournal.Application.Entities;

namespace FoodJournal.BlazorApp.Components.Meals.Models;

public class UpdateMealInputModel
{
    [Required]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Please enter a meal name.")]
    [DataType(DataType.Text)]
    public string Name { get; set; } = string.Empty;

    [MinLength(1, ErrorMessage = "Please select at least one food item.")]
    public Collection<Food> Foods { get; } = [];
}
