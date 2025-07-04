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

        var sqlServer = builder.AddSqlServer(ServiceNames.DatabaseProvider)
                               .WithHostPort(ServiceNames.DatabasePort)
                               .WithLifetime(ContainerLifetime.Persistent)
                               .WithContainerName("foodjournal-sqlserver");

        var journalDatabase = sqlServer.AddDatabase(ServiceNames.DatabaseName);

        var migratorApp = builder.AddProject<FoodJournal_DatabaseMigrator>(ServiceNames.DatabaseMigratorAppName)
                                 .WithReference(journalDatabase)
                                 .WaitFor(journalDatabase);

        var blazorApp = builder.AddProject<FoodJournal_BlazorApp>(ServiceNames.WebAppName, "https")
                               .WithExternalHttpEndpoints()
                               .WithReference(journalDatabase)
                               .WaitFor(migratorApp);

        await builder.Build().RunAsync();
    }
}