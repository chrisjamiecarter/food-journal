using FoodJournal.Application.Database;
using FoodJournal.Application.Entities;
using FoodJournal.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FoodJournal.Application;

public static class ApplicationInstaller
{
    public static IHostApplicationBuilder AddApplication(this IHostApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        builder.AddSqlServerDbContext<ApplicationDbContext>(ServiceNames.DatabaseName);

        builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                        .AddEntityFrameworkStores<ApplicationDbContext>()
                        .AddSignInManager()
                        .AddDefaultTokenProviders();

        return builder;
    }
}
