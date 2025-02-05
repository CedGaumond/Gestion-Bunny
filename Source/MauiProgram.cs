using Gestion_Bunny.Services;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

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

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Services.AddSingleton<PageTitleService>();
        builder.Logging.AddDebug();
        builder.Logging.SetMinimumLevel(LogLevel.Debug);

        AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        {
            Console.WriteLine($"[ERREUR GLOBALE] {e.ExceptionObject}");
            Debug.WriteLine($"[ERREUR GLOBALE] {e.ExceptionObject}");
        };

#endif

        return builder.Build();
	}
}