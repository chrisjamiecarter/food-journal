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

        var sqlServer = builder.AddSqlServer(AppHostConstants.DatabaseProvider, port: AppHostConstants.DatabasePort)
                               .WithContainerName(AppHostConstants.DatabaseContainerName)
                               .WithLifetime(ContainerLifetime.Persistent)
                               .WithDataVolume();

        var journalDatabase = sqlServer.AddDatabase(AppHostConstants.DatabaseName);

        var migratorApp = builder.AddProject<FoodJournal_DatabaseMigrator>(AppHostConstants.DatabaseMigratorAppName)
                                 .WithReference(journalDatabase)
                                 .WaitFor(journalDatabase);

        var blazorApp = builder.AddProject<FoodJournal_BlazorApp>(AppHostConstants.WebAppName)
                               .WithReference(journalDatabase)
                               .WaitFor(migratorApp);

        await builder.Build().RunAsync();
    }
}
