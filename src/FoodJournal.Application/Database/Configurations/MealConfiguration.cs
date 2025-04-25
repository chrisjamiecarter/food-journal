using FoodJournal.Application.Database.Constants;
using FoodJournal.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodJournal.Application.Database.Configurations;

/// <summary>
/// Configures a <see cref="Meal"/> table definition in the database.
/// </summary>
internal sealed class MealConfiguration : IEntityTypeConfiguration<Meal>
{
    public void Configure(EntityTypeBuilder<Meal> builder)
    {
        builder.ToTable(Tables.Meal, Schemas.Core);

        builder.HasKey(pk => pk.Id);

        builder.Property(p => p.Date)
               .IsRequired();

        builder.Property(p => p.Type)
               .IsRequired()
               .HasConversion<int>();

        builder.HasMany(e => e.Foods)
               .WithMany(e => e.Meals);
    }
}
