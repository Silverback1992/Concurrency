using System.Diagnostics;
using System.Windows;

namespace ConfigureAwaitWPF;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = App.Current.Services.GetService(typeof(MainWindowViewModel));
    }

    private async void CrashButton_Click(object sender, RoutedEventArgs e)
    {
        Debug.Print("We are on the UI Thread.");

        var currentContext = SynchronizationContext.Current;

        if (currentContext != null)
        {
            Debug.Print("UI Thread.");
        }

        Debug.Print($"UI Thread Id: {Environment.CurrentManagedThreadId}");

        // We force the rest of the method onto a background thread
        await Task.Delay(1000).ConfigureAwait(false);

        Debug.Print("We are now on a background thread.");

        var currentContext2 = SynchronizationContext.Current;

        if (currentContext2 == null)
        {
            Debug.Print("Not a UI Thread.");
        }

        Debug.Print($"Thread Id: {Environment.CurrentManagedThreadId}");

        // 💥 CRASH! We are touching the UI control directly!
        MyCrashLabel.Text = "This will explode immediately.";
    }
}