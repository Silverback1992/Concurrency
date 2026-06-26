using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Concurrency.ValueTask;

public class BadValueTaskArchitecture
{
    // FATAL FLAW 1: This method ALWAYS hits hardware. 
    // Using a ValueTask here actually makes it SLOWER than a normal Task, 
    // because the compiler has to build the Task on the Heap AND wrap it in the struct.
    [AsyncMethodBuilder(typeof(PoolingAsyncValueTaskMethodBuilder<>))]
    public async ValueTask<string> DownloadNetworkDataAsync()
    {
        await Task.Delay(1000).ConfigureAwait(false);
        return "Critical Payload";
    }

    public async Task ExecuteTrapAsync()
    {
        // FATAL FLAW 2: Storing the ValueTask in a variable instead of awaiting it immediately.
        ValueTask<string> pendingDownload = DownloadNetworkDataAsync();

        Debug.Print("Awaiting the first time...");

        // This will succeed. The data is retrieved.
        string result1 = await pendingDownload;
        Debug.Print($"First result: {result1}");

        Debug.Print("Awaiting the exact same work a second time...");

        // 💥 THE CRASH!
        // You can await a standard Task 100 times. You can ONLY await a ValueTask ONCE.
        string result2 = await pendingDownload;

        Debug.Print("You will never see this line.");
    }
}
