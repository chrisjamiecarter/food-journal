using FoodJournal.Application.Entities;

namespace FoodJournal.DatabaseMigrator.Models;

internal static class FoodSeedMappings
{
    public static Food ToDomain(this FoodSeed seed)
    {
        return new Food(Guid.CreateVersion7(),
                        seed.Name,
                        seed.Base64Image,
                        seed.ServingSize,
                        seed.Calories,
                        seed.Carbs,
                        seed.Protein,
                        seed.Fat);
    }
}
