using System.Diagnostics;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FoodJournal.AppHost.Extensions;

internal static class ResourceBuilderExtensions
{
    internal static IResourceBuilder<T> WithScalar<T>(this IResourceBuilder<T> builder) where T : IResourceWithEndpoints
    {
        return builder.WithOpenApiDocs("scalar-docs", "Scalar API Documentation", "scalar/v1");
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Not required.")]
    private static IResourceBuilder<T> WithOpenApiDocs<T>(this IResourceBuilder<T> builder, string name, string displayName, string openApiUiPath) where T : IResourceWithEndpoints
    {
        return builder.WithCommand(name, displayName, async _ =>
        {
            try
            {
                var endpoint = builder.GetEndpoint("https");

                var url = $"{endpoint.Url}/{openApiUiPath}";

                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });

                return new ExecuteCommandResult
                {
                    Success = true
                };
            }
            catch (Exception exception)
            {
                return new ExecuteCommandResult
                {
                    Success = false,
                    ErrorMessage = exception.ToString()
                };
            }
        },
        new CommandOptions
        {
            UpdateState = context => context.ResourceSnapshot.HealthStatus == HealthStatus.Healthy ? ResourceCommandState.Enabled : ResourceCommandState.Disabled,
            IconName = "Document",
            IconVariant = IconVariant.Filled
        });
    }
}
