using Aspire.Hosting;
using FoodJournal.AppHost.Extensions;
using FoodJournal.Common;
using Projects;

namespace FoodJournal.AppHost;

/// <summary>
/// Configures and runs the distributed application.
/// </summary>
internal static class Program
{
    private static async Task Main(string[] args)
    {
        var builder = DistributedApplication.CreateBuilder(args);

        var sqlServer = builder.AddSqlServer(ServiceNames.DatabaseProvider, port: ServiceNames.DatabasePort)
                               .WithContainerName("foodjournal-sqlserver")
                               .WithLifetime(ContainerLifetime.Persistent)
                               .WithDataVolume();

        var journalDatabase = sqlServer.AddDatabase(ServiceNames.DatabaseName);

        var migratorApp = builder.AddProject<FoodJournal_DatabaseMigrator>(ServiceNames.DatabaseMigratorAppName)
                                 .WithReference(journalDatabase)
                                 .WaitFor(journalDatabase);

        var blazorApp = builder.AddProject<FoodJournal_BlazorApp>(ServiceNames.WebAppName)
                               .WithReference(journalDatabase)
                               .WaitFor(migratorApp);

        await builder.Build().RunAsync();
    }
}