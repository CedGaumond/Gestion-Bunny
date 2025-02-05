public static class DatabaseConfiguration
{
    public static string GetConnectionString()
    {
        string databasePath = "" +
            "Host=127.0.0.1;" + // Make sure the IP and port are correct
            "Port=5432;" +
            "Username=postgres;" + // Use the correct username
            "Password=Lolipopi60;" + // Use the correct password
            "Database=bunny_db;";  // Ensure the database exists and is accessible

        return databasePath;
    }

}
