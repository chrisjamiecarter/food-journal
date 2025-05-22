using FoodJournal.Application.Database.Constants;
using FoodJournal.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodJournal.Application.Database.Configurations;

/// <summary>
/// Configures an <see cref="ApplicationUser"/> table definition in the database.
/// </summary>
internal sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable(Tables.ApplicationUser, Schemas.Auth);

        builder.HasMany(e => e.Meals)
               .WithOne(e => e.User)
               .HasForeignKey(fk => fk.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
