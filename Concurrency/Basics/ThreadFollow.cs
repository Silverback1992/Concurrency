namespace Concurrency.Basics;

public static class ThreadFollow
{
    public static async Task DoWorkAsync()
    {
        Console.WriteLine("DoWork method is doing work.");

        Console.WriteLine("And some more work.");

        Console.WriteLine($"Thread Id: {Environment.CurrentManagedThreadId}");

        await MiddleMethodAsync();

        Console.WriteLine($"Should be a different Thread Id: {Environment.CurrentManagedThreadId}");
    }

    private static async Task MiddleMethodAsync()
    {
        Console.WriteLine("MiddleMethod method is doing work");

        Console.WriteLine($"Should be the same Thread Id: {Environment.CurrentManagedThreadId}");

        await BottomMethodAsync();
    }

    private static async Task BottomMethodAsync()
    {
        Console.WriteLine($"Still should be the same Thread Id: {Environment.CurrentManagedThreadId}");

        await Task.Delay(1000);
    }
}
