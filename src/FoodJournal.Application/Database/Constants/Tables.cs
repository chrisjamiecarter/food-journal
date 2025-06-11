namespace FoodJournal.Application.Database.Constants;

/// <summary>
/// Defines the table names used by the database.
/// </summary>
internal static class Tables
{
    public static readonly string ApplicationUser = "User";
    public static readonly string ApplicationUserClaim = "UserClaim";
    public static readonly string ApplicationUserLogin = "UserLogin";
    public static readonly string ApplicationUserRole = "UserRole";
    public static readonly string ApplicationUserToken = "UserToken";
    public static readonly string EntityFrameworkCoreMigration = "Migration";
    public static readonly string Food = "Food";
    public static readonly string Meal = "Meal";
    public static readonly string QuickMeal = "QuickMeal";
    public static readonly string Role = "Role";
    public static readonly string RoleClaim = "RoleClaim";
}
