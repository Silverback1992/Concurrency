using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace ConfigureAwaitWPF;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        Services = ConfigureServices();
    }

    public static new App Current => (App)Application.Current;

    /// <summary>
    /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
    /// </summary>
    public IServiceProvider Services { get; }

    /// <summary>
    /// Configures the services for the application.
    /// </summary>
    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        //services.AddSingleton<ISettingsService, SettingsService>();
        services.AddTransient<MainWindowViewModel>();

        return services.BuildServiceProvider();
    }
}

