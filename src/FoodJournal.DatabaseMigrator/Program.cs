using FoodJournal.Application.Database;
using FoodJournal.Common;
using FoodJournal.ServiceDefaults;

namespace FoodJournal.DatabaseMigrator;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.AddServiceDefaults();

        builder.Services.AddHostedService<Worker>();

        builder.Services.AddOpenTelemetry().WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

        builder.AddSqlServerDbContext<ApplicationDbContext>(ServiceNames.DatabaseName);

        var host = builder.Build();

        await host.RunAsync();
    }
}