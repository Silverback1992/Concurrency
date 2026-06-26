using System.Diagnostics;

namespace Concurrency.ConfigureAwait;

public static class ConfigureAwaitGoodExample
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

        await Task.Delay(5000).ConfigureAwait(false);

        var currentContext2 = SynchronizationContext.Current;

        if (currentContext2 == null)
        {
            Debug.Print("Not a UI Thread.");
        }

        Debug.Print($"Thread Id: {Environment.CurrentManagedThreadId}");

        Thread.Sleep(5000);
    }
}
