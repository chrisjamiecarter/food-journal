using FoodJournal.Application.Database.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodJournal.Application.Database.Configurations;

/// <summary>
/// Configures an <see cref="IdentityUserClaim"/> table definition in the database.
/// </summary>
internal class IdentityUserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder)
    {
        builder.ToTable(Tables.ApplicationUserClaim, Schemas.Auth);
    }
}
