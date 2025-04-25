using FoodJournal.Application.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodJournal.Application.Database;

/// <summary>
/// The Entity Framework database context for the application.
/// Configures the models and provides access to the database tables.
/// </summary>
/// <param name="options">The <see cref="DbContextOptions{ApplicationDbContext}"/> used to configure the database context.</param>
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Food> Foods { get; set; } = default!;
    public DbSet<Meal> Meals { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        
        builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
