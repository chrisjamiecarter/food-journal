using FoodJournal.Application.Database;
using FoodJournal.Common;
using FoodJournal.DatabaseMigrator.Services;
using FoodJournal.ServiceDefaults;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FoodJournal.DatabaseMigrator;

internal static class Program
{
    internal static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.AddServiceDefaults();

        builder.AddSqlServerDbContext<ApplicationDbContext>(ServiceNames.DatabaseName);

        builder.Services.AddHostedService<MigrationService>();

        var app = builder.Build();

        await app.RunAsync();
    }
}
