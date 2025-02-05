using Gestion_Bunny.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Gestion_Bunny.Modeles;  // Ensure you include the namespace where EmployeeContext is located

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

        // Get the connection string for PostgreSQL from DatabaseConfiguration (ensure it's configured correctly)
        string connectionString = DatabaseConfiguration.GetConnectionString();  // Or use builder.Configuration.GetConnectionString("DefaultConnection");

        // Add the DbContext for PostgreSQL using Npgsql
        builder.Services.AddDbContext<EmployeeContext>(options =>
            options.UseNpgsql(connectionString)); // Make sure EmployeeContext is set up with PostgreSQL support
        
        // Register the EmployeeService and AuthenticationService for Dependency Injection
        builder.Services.AddScoped<IEmployeeService, EmployeeService>();
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        builder.Services.AddSingleton<PageTitleService>();

        // If using Debug mode, add developer tools and logging
    #if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools(); 
        builder.Logging.AddDebug();
        builder.Logging.SetMinimumLevel(LogLevel.Debug);
    #endif

        return builder.Build();
	}
}
