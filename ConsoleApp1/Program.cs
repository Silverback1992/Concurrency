Console.WriteLine("Hello, World!");

Console.WriteLine($"Thread id: {Environment.CurrentManagedThreadId}");

await Derp().ConfigureAwait(ConfigureAwaitOptions.ForceYielding);

Console.WriteLine($"Thread id: {Environment.CurrentManagedThreadId}");

Console.WriteLine("Derp completed.");


static async Task Derp()
{
    Console.WriteLine($"Thread id: {Environment.CurrentManagedThreadId}");
}