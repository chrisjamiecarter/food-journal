using FoodJournal.Application.Database.Constants;
using FoodJournal.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodJournal.Application.Database.Configurations;

/// <summary>
/// Configures a <see cref="Food"/> table definition in the database.
/// </summary>
internal sealed class FoodConfiguration : IEntityTypeConfiguration<Food>
{
    public void Configure(EntityTypeBuilder<Food> builder)
    {
        builder.ToTable(Tables.Food, Schemas.Core);

        builder.HasKey(pk => pk.Id);

        builder.Property(p => p.Name)
               .IsRequired();

        builder.HasMany(e => e.Meals)
               .WithMany(e => e.Foods);
    }
}
