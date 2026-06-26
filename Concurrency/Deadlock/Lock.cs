namespace Concurrency.Deadlock;

public static class Lock
{
    public static void Deadlock()
    {
        Task task = WaitAsync();

        task.Wait();
    }

    private static async Task WaitAsync()
    {
        await Task.Delay(1000);
    }
}
