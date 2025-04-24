namespace FoodJournal.Common;

public static class ServiceNames
{
    internal static class Database
    {
        public const string Name = "postgres";
        public const string Image = "postgres";
        public const string ImageTag = "latest";
        public const string ServerName = "foodjournal-db";
    }

    public const string FoodJournal = "FoodJournal";

    public const string OutputCache = "outputcahce";
}
