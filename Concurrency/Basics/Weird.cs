namespace Concurrency.Basics;

public static class Weird
{
    public static void MainWeird()
    {
        Task task = SubWeirdAsync();

        Console.WriteLine("Hi");

        Console.WriteLine("Some string");

        task.Wait();
    }

    private static async Task SubWeirdAsync()
    {
        await Task.Delay(5000);

        Console.WriteLine("Some string 5 sec later");
    }
}
