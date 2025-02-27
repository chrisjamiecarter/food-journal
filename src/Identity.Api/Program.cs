using FoodJournal.ServiceDefaults;
using Identity.Api.Data;
using Identity.Api.Extensions;
using Identity.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.Api;

internal static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddServiceDefaults();

        builder.Services.AddControllers();

        builder.Services.AddAuthentication()
                        .AddBearerToken(IdentityConstants.BearerScheme);

        builder.Services.AddAuthorizationBuilder();

        builder.AddNpgsqlDbContext<ApplicationDbContext>("identitydb");
        
        builder.Services.AddMigration<ApplicationDbContext>();

        builder.Services.AddIdentityCore<ApplicationUser>()
                        .AddEntityFrameworkStores<ApplicationDbContext>()
                        .AddApiEndpoints();

        var app = builder.Build();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapIdentityApi<ApplicationUser>();

        app.MapGet("/", () => "Identity API is running.");

        //app.MapGet("/", (ClaimsPrincipal user) => $"Hello {user.Identity!.Name}")
        //   .RequireAuthorization();

        app.MapControllers();

        app.Run();
    }
}
