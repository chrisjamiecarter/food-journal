using Aspire.Hosting;
using FoodJournal.AppHost.Extensions;

namespace FoodJournal.AppHost;

/// <summary>
/// Configures and runs the distributed application.
/// </summary>
internal static class Program
{
    private static async Task Main(string[] args)
    {
        var builder = DistributedApplication.CreateBuilder(args);

        var postgres = builder.AddPostgres("postgres")
                              .WithImage("postgres")
                              .WithImageTag("latest")
                              .WithLifetime(ContainerLifetime.Persistent);
        
        var identityDb = postgres.AddDatabase("identitydb");

        var identityApi = builder.AddProject<Projects.Identity_Api>("identityapi", "https")
                                 .WithExternalHttpEndpoints()
                                 .WithReference(identityDb)
                                 .WithScalar()
                                 .WaitFor(identityDb);

        var webApp = builder.AddProject<Projects.WebApp>("webapp", "https")
                            .WithExternalHttpEndpoints()
                            .WithReference(identityDb)
                            .WaitFor(identityApi);

        await builder.Build().RunAsync();
    }
}