using Bogus;
using FoodJournal.Application.Database;
using FoodJournal.Application.Entities;
using FoodJournal.Application.Enums;
using FoodJournal.DatabaseMigrator.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FoodJournal.DatabaseMigrator.Services;

internal class MigrationService : IHostedService
{
    private static readonly int Seed = 19890309;

    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<MigrationService> _logger;

    public MigrationService(IServiceProvider serviceProvider, ILogger<MigrationService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        _logger.LogInformation("Starting database migrations");

        await dbContext.Database.MigrateAsync(cancellationToken);

        _logger.LogInformation("Finished database migrations");

        _logger.LogInformation("Starting database seeding");

        await SeedDatabaseAsync(dbContext, cancellationToken);

        _logger.LogInformation("Finished database seeding");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task SeedDatabaseAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken)
    {
        if (!await dbContext.Foods.AnyAsync(cancellationToken) && !await dbContext.Meals.AnyAsync(cancellationToken))
        {
            _logger.LogInformation("Starting seeding foods");

            var foods = Foods.Names.Select(x => new Food(Guid.CreateVersion7(), x));

            await dbContext.Foods.AddRangeAsync(foods, cancellationToken);
            
            await dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Finished seeding foods");

            _logger.LogInformation("Starting seeding meals");

            var mealFaker = new Faker<Meal>()
                .UseSeed(Seed)
                .RuleFor(e => e.Id, f => Guid.CreateVersion7())
                .RuleFor(e => e.Date, f => f.Date.Past(1))
                .RuleFor(e => e.Type, f => f.PickRandom<MealType>())
                .RuleFor(e => e.Foods, f => f.Make(f.Random.Int(1, 5), () => f.PickRandom(foods)));

            var meals = mealFaker.Generate(100);

            await dbContext.Meals.AddRangeAsync(meals, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Finished seeding meals");
        }
    }
}
