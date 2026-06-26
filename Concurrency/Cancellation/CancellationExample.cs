namespace Concurrency.Cancellation;

public class CancellationExample
{
    public static async Task ExecuteAsync()
    {
        // 1. We build the Detonator
        using CancellationTokenSource cts = new();

        Console.WriteLine("Starting download...");

        // 2. We start the background work and hand it the Dynamite Wire (cts.Token)
        Task downloadTask = DownloadMassiveFileAsync(cts.Token);

        // Simulating the user looking at the screen for 2 seconds...
        await Task.Delay(2000);

        // 3. The user clicks "Cancel UI Button"
        Console.WriteLine("\n[UI Thread] User clicked Cancel! Pressing the detonator...");
        cts.Cancel();

        try
        {
            // We must always await the task to observe its final state or catch its explosion
            await downloadTask;
            Console.WriteLine("Download finished successfully.");
        }
        catch (OperationCanceledException)
        {
            // 5. We safely catch the explosion
            Console.WriteLine("The task was successfully aborted.");
        }
    }

    private static async Task DownloadMassiveFileAsync(CancellationToken token)
    {
        for (int i = 1; i <= 10; i++)
        {
            // 4. THE CRITICAL STEP: The background work checks the wire.
            // If cts.Cancel() was called, this exact line throws an OperationCanceledException instantly.
            token.ThrowIfCancellationRequested();

            Console.WriteLine($"Downloading chunk {i} of 10...");

            // We also pass the token down into built-in .NET methods so they can listen to the wire too!
            await Task.Delay(500, token);
        }
    }
}