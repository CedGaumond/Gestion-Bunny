﻿using Gestion_Bunny.Services;
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

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(connectionString)
                   .EnableDetailedErrors()
                   .EnableSensitiveDataLogging()
                   .LogTo(Console.WriteLine, LogLevel.Information)
        );


        // Register the EmployeeService and AuthenticationService for Dependency Injection
        builder.Services.AddScoped<IIngredientService, IngredientService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddSingleton<IOrderCartService, OrderCartService>();
        builder.Services.AddSingleton<IBillService, BillService>();
        builder.Services.AddSingleton<IPDFService, PDFService>();
        builder.Services.AddScoped<IRecipeService, RecipeService>();
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        builder.Services.AddScoped<INotificationService, NotificationService>();
        builder.Services.AddSingleton<PageTitleService>();


        builder.Services.AddSingleton<IIngredientValidationService, IngredientValidationService>();
        builder.Services.AddSingleton<IScheduleService, ScheduleService>();
        builder.Services.AddSingleton<IProfileService, ProfileService>();
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
