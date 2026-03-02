using FoodJournal.Application.Database.Constants;
using FoodJournal.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodJournal.Application.Database.Configurations;

/// <summary>
/// Configures a <see cref="QuickMeal"/> table definition in the database.
/// </summary>
internal sealed class QuickMealConfiguration : IEntityTypeConfiguration<QuickMeal>
{
    public void Configure(EntityTypeBuilder<QuickMeal> builder)
    {
        builder.ToTable(Tables.QuickMeal, Schemas.Core);

        builder.HasKey(pk => pk.Id);

        builder.Property(p => p.Name)
               .IsRequired();

        builder.Property(p => p.UserId)
               .IsRequired();

        builder.HasMany(e => e.Foods)
               .WithMany(e => e.QuickMeals)
               .UsingEntity<Dictionary<string, object>>(
                    right => right
                        .HasOne<Food>()
                        .WithMany()
                        .HasForeignKey(ForeignKeys.FoodsId)
                        .OnDelete(DeleteBehavior.Cascade),
                    left => left
                        .HasOne<QuickMeal>()
                        .WithMany()
                        .HasForeignKey(ForeignKeys.QuickMealsId)
                        .OnDelete(DeleteBehavior.Cascade))
               .ToTable(Tables.FoodQuickMeal, Schemas.Core);
    }
}
