using Gestion_Bunny.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Gestion_Bunny.Modeles;


namespace Gestion_Bunny;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddBlazorBootstrap();


        string connectionString = DatabaseConfiguration.GetConnectionString();

        // Add the DbContext for PostgreSQL using Npgsql
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString)); // Make sure ApplicationDbContext is set up with PostgreSQL support

        // Register the EmployeeService and AuthenticationService for Dependency Injection
        builder.Services.AddScoped<IEmployeeService, EmployeeService>();
        builder.Services.AddScoped<IItemService, ItemService>();
        builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
        builder.Services.AddSingleton<PageTitleService>();
        

        builder.Services.AddSingleton<IIngredientService, IngredientService>();

        builder.Services.AddSingleton<AuthenticationState>();

        // If using Debug mode, add developer tools and logging
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
        builder.Logging.SetMinimumLevel(LogLevel.Debug);
#endif

        return builder.Build();
    }
}
