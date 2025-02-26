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

        var webApp = builder.AddProject<Projects.WebApp>("webapp")
                            .WithReference(identityDb);

        await builder.Build().RunAsync();
    }
}