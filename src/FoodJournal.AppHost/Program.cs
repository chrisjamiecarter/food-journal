namespace FoodJournal.AppHost;

/// <summary>
/// Configures and runs the distributed application.
/// </summary>
internal static class Program
{
    private static async Task Main(string[] args)
    {
        var builder = DistributedApplication.CreateBuilder(args);

        builder.AddProject<Projects.WebApp>("webapp");

        await builder.Build().RunAsync();
    }
}