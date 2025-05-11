using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;

namespace FoodJournal.Application.Entities;


/// <summary>
/// Represents a user in the application, extending the <see cref="IdentityUser"/> class.
/// </summary>
public class ApplicationUser : IdentityUser
{
    public Collection<Meal> Meals { get; } = [];
}
