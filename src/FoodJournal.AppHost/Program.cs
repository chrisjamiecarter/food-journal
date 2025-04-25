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
                               .WithLifetime(ContainerLifetime.Persistent);

        var journalDatabase = sqlServer.AddDatabase(ServiceNames.DatabaseName);

        var migratorApp = builder.AddProject<FoodJournal_DatabaseMigrator>(ServiceNames.DatabaseMigratorAppName)
                                 .WithReference(journalDatabase)
                                 .WaitFor(journalDatabase);

        var blazorApp = builder.AddProject<FoodJournal_BlazorApp>(ServiceNames.WebAppName, "https")
                               .WithExternalHttpEndpoints()
                               .WithReference(journalDatabase)
                               .WaitFor(migratorApp);

        //var identityApi = builder.AddProject<Projects.Identity_Api>("identityapi", "https")
        //                         .WithExternalHttpEndpoints()
        //                         .WithReference(identityDb)
        //                         .WithScalar()
        //                         .WaitFor(identityDb);

        //var webApp = builder.AddProject<Projects.WebApp>("webapp", "https")
        //                    .WithExternalHttpEndpoints()
        //                    .WithReference(identityDb)
        //                    .WaitFor(identityApi);

        await builder.Build().RunAsync();
    }
}