public static class DatabaseConfiguration
{
    public static string GetConnectionString()
    {
        string databasePath = "" +
            "Host=127.0.0.1;" +
            "Port=5432;" +
            "Username=postgres;" +
            "Password=UlGBqeXlkG;" +
            "Database=bunny_db;";

        return databasePath;
    }

}
