using System.Diagnostics;
using FoodJournal.Application.Database;
using FoodJournal.Application.Entities;
using FoodJournal.DatabaseMigrator.Models;
using FoodJournal.DatabaseMigrator.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FoodJournal.DatabaseMigrator;

internal class Worker : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    //private const int Seed = 19890309;

    private static readonly ActivitySource ActivitySource = new(ActivitySourceName);

    private readonly IServiceProvider _serviceProvider;
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly SeedDataOptions _options;

    public Worker(IServiceProvider serviceProvider, IHostApplicationLifetime hostApplicationLifetime, IOptions<SeedDataOptions> options)
    {
        _serviceProvider = serviceProvider;
        _hostApplicationLifetime = hostApplicationLifetime;
        _options = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var activity = ActivitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await RunMigrationAsync(dbContext, stoppingToken);
            await SeedDatabaseAsync(dbContext, stoppingToken);
        }
        catch (Exception exception)
        {
            activity?.AddException(exception);
            throw;
        }

        _hostApplicationLifetime.StopApplication();
    }

    private async Task RunMigrationAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await dbContext.Database.MigrateAsync(cancellationToken);
        });
    }

    private async Task SeedDatabaseAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken)
    {
        var dbFoods = await dbContext.Foods.AsNoTracking().ToListAsync(cancellationToken);
        
        var seedFoods = _options.Foods.Select(x => x.ToDomain()).ToList();

        var foods = seedFoods.Where(x => !dbFoods.Any(y => y.Name == x.Name)).ToList();

        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
            await dbContext.Foods.AddRangeAsync(foods, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        });
    }

}
