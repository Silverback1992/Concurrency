using System.Diagnostics;

namespace Concurrency.ConfigureAwait;

public static class ConfigureAwaitBadExample
{
    public static async Task DoWorkAsync()
    {
        Debug.Print("Quick work.");

        Debug.Print($"UI Thread Id: {Environment.CurrentManagedThreadId}");

        var currentContext = SynchronizationContext.Current;

        if (currentContext != null)
        {
            Debug.Print("UI Thread.");
        }

        Debug.Print("Heavy work.");

        await SubWorkAsync();

        var currentContext2 = SynchronizationContext.Current;

        if (currentContext != null)
        {
            Debug.Print("UI Thread.");
        }

        Debug.Print($"UI Thread Id: {Environment.CurrentManagedThreadId}");

        Thread.Sleep(5000);
    }

    private static async Task SubWorkAsync()
    {
        await Task.Delay(5000).ConfigureAwait(false);

        Debug.Print($"Thread Id: {Environment.CurrentManagedThreadId}");
    }
}
