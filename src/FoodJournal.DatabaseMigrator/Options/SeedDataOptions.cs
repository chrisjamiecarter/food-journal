using FoodJournal.DatabaseMigrator.Models;

namespace FoodJournal.DatabaseMigrator.Options;

internal sealed class SeedDataOptions
{
    public List<FoodSeed> Foods { get; set; } = [];
}
