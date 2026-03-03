using FoodJournal.Application.Database;
using FoodJournal.Application.Entities;
using FoodJournal.Application.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FoodJournal.Application.Services;

public sealed record SeedTestUserDataResponse(
    bool Success,
    string Message,
    string Email,
    string Password);

public interface ISeedService
{
    Task<SeedTestUserDataResponse> SeedTestUserDataAsync(CancellationToken cancellationToken);
}

[System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "CA5394:Do not use insecure randomness", Justification = "No security required.")]
internal sealed class SeedService(
    UserManager<ApplicationUser> userManager,
    ApplicationDbContext dbContext,
    ILogger<SeedService> logger) : ISeedService
{
    private const string TestEmail = "test@example.com";
    private const string TestPassword = "T£$tP@$$0rd";
    private const string TestUsername = "testuser";
    private const int DaysOfData = 60;

    private static readonly SeedTestUserDataResponse TestUserAlreadyExistsResponse = FailureResponse("Test user already exists.");
    private static readonly SeedTestUserDataResponse FailedToCreateUserResponse = FailureResponse("Failed to create test user.");
    private static readonly SeedTestUserDataResponse NoFoodsExistsResponse = FailureResponse("No foods found in the database.");
    private static readonly SeedTestUserDataResponse UserCreatedResponse = SuccessResponse("Test user created.");
    private static SeedTestUserDataResponse FailureResponse(string message) => Response(false, message);
    private static SeedTestUserDataResponse SuccessResponse(string message) => Response(true, message);
    private static SeedTestUserDataResponse Response(bool success, string message) => new(success, message, TestEmail, TestPassword);

    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly ILogger<SeedService> _logger = logger;

    public async Task<SeedTestUserDataResponse> SeedTestUserDataAsync(CancellationToken cancellationToken)
    {
        if (await _userManager.FindByEmailAsync(TestEmail) is not null || await _userManager.FindByNameAsync(TestUsername) is not null)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Test user already exists with email {Email} or username {Username}.", TestEmail, TestUsername);
            }
            return TestUserAlreadyExistsResponse;
        }

        var user = new ApplicationUser
        {
            Email = TestEmail,
            EmailConfirmed = true,
            UserName = TestUsername,
        };

        var userCreationResult = await _userManager.CreateAsync(user, TestPassword);
        if (!userCreationResult.Succeeded)
        {
            _logger.LogError("Failed to create test user with email {Email} and username {Username}. {Errors}", TestEmail, TestUsername, string.Join(", ", userCreationResult.Errors.Select(e => e.Description)));
            return FailedToCreateUserResponse;
        }

        var foods = await _dbContext.Foods.ToListAsync(cancellationToken);
        if (foods.Count == 0)
        {
            return NoFoodsExistsResponse;
        }

        // Create journal entries for past 60 days, starting from yesterday.
        var startDate = DateTime.Today.AddDays(-1);

        for (int i = 0; i < DaysOfData; i++)
        {
            var date = startDate.AddDays(-i);

            await CreateMealWithRandomFoodsAsync(user.Id, date, MealType.Breakfast, foods, 1, 3, cancellationToken);
            await CreateMealWithRandomFoodsAsync(user.Id, date, MealType.Lunch, foods, 1, 3, cancellationToken);
            await CreateMealWithRandomFoodsAsync(user.Id, date, MealType.Dinner, foods, 1, 4, cancellationToken);
            if (Random.Shared.Next(2) == 0)
            {
                await CreateMealWithRandomFoodsAsync(user.Id, date, MealType.Snack, foods, 1, 2, cancellationToken);
            }
        }

        // Create quick meal entries.
        await CreateQuickMealsWithRandomFoodsAsync(user.Id, foods, cancellationToken);

        return UserCreatedResponse;
    }

    private async Task CreateMealWithRandomFoodsAsync(
        string userId,
        DateTime date,
        MealType mealType,
        List<Food> availableFoods,
        int minFoods,
        int maxFoods,
        CancellationToken cancellationToken)
    {
        // Check if meal already exists for this date and type.
        if (await _dbContext.Meals
            .Where(m => m.UserId == userId && m.Date.Date == date.Date && m.Type == mealType)
            .AnyAsync(cancellationToken))
        {
            return;
        }

        // Create meal and randomly select foods.
        var meal = new Meal(Guid.CreateVersion7(), userId, date, mealType);

        var foodCount = Random.Shared.Next(minFoods, maxFoods + 1);
        var selectedFoods = availableFoods.OrderBy(_ => Random.Shared.Next()).Take(foodCount).ToList();

        foreach (var food in selectedFoods)
        {
            meal.Foods.Add(food);
        }

        _dbContext.Meals.Add(meal);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task CreateQuickMealsWithRandomFoodsAsync(
        string userId,
        List<Food> availableFoods,
        CancellationToken cancellationToken)
    {
        var quickMealCount = Random.Shared.Next(3, 6);
        for (int i = 0; i < quickMealCount; i++)
        {
            var foodCount = Random.Shared.Next(2, 4);
            var selectedFoods = availableFoods.OrderBy(_ => Random.Shared.Next()).Take(foodCount).ToList();

            var name = string.Join(" & ", selectedFoods.Select(food => food.Name));
            var quickMeal = new QuickMeal(Guid.CreateVersion7(), userId, name);

            foreach (var food in selectedFoods)
            {
                quickMeal.Foods.Add(food);
            }

            _dbContext.QuickMeals.Add(quickMeal);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
