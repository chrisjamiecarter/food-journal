﻿using FoodJournal.Application.Database;
using FoodJournal.Application.Entities;
using FoodJournal.Application.Repositories;
using FoodJournal.Application.Services;
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

        builder.Services.AddScoped<IFoodRepository, FoodRepository>();
        builder.Services.AddScoped<IMealRepository, MealRepository>();
        builder.Services.AddScoped<IQuickMealRepository, QuickMealRepository>();

        builder.Services.AddScoped<IFoodService, FoodService>();
        builder.Services.AddScoped<IMealService, MealService>();
        builder.Services.AddScoped<IQuickMealService, QuickMealService>();

        return builder;
    }
}
