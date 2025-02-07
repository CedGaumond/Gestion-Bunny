using Gestion_Bunny.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Gestion_Bunny.Modeles;
using Gestion_Bunny.Data;


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


        builder.Services.AddDbContext<EmployeeContext>(options =>
            options.UseNpgsql(connectionString));


        builder.Services.AddDbContext<IngredientContext>(options =>
            options.UseNpgsql(connectionString));


        builder.Services.AddScoped<IEmployeeService, EmployeeService>();
        builder.Services.AddScoped<IIngredientService, IngredientService>();


        builder.Services.AddScoped<IEmployeeService, EmployeeService>();
        builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
        builder.Services.AddSingleton<PageTitleService>();


        builder.Services.AddSingleton<AuthenticationState>();


#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
        builder.Logging.SetMinimumLevel(LogLevel.Debug);
#endif

        return builder.Build();
    }
}
