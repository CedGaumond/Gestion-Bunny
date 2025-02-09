using System.Diagnostics;
using Microsoft.JSInterop;

namespace Gestion_Bunny;

public partial class App : Application
{
    private readonly IJSRuntime _jsRuntime;

    public App(IJSRuntime jsRuntime)
    {
        InitializeComponent();
        _jsRuntime = jsRuntime;

        // Set up global exception handling
        AppDomain.CurrentDomain.UnhandledException += HandleUnhandledException;
        TaskScheduler.UnobservedTaskException += HandleUnobservedTaskException;
    }

    private void HandleUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        var ex = e.ExceptionObject as Exception;
        if (ex != null)
        {
            MainThread.BeginInvokeOnMainThread(async () => 
            {
                try 
                {
                    await _jsRuntime.InvokeVoidAsync("window.dispatchEvent", 
                        new DotNetStreamJsonException 
                        { 
                            type = ex.GetType().Name,
                            message = ex.Message, 
                            stack = ex.StackTrace ?? "No stack trace available"
                        }
                    );
                }
                catch 
                {
                    // Fallback logging
                    System.Diagnostics.Debug.WriteLine($"Error handling failed: {ex.Message}");
                }
            });
        }
    }

    private void HandleUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
    {
        HandleUnhandledException(sender, new UnhandledExceptionEventArgs(e.Exception, false));
    }

    public class DotNetStreamJsonException
    {
        public string type { get; set; } = string.Empty;
        public string message { get; set; } = string.Empty;
        public string stack { get; set; } = string.Empty;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new MainPage()) { Title = "Gestion-Bunny" };
    }
}