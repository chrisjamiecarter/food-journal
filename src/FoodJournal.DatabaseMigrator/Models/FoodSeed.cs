namespace FoodJournal.DatabaseMigrator.Models;

internal sealed record FoodSeed(string Name,
                                string Base64Image,
                                string ServingSize = "",
                                int Calories = 0,
                                float Carbs = 0,
                                float Protein = 0,
                                float Fat = 0);
