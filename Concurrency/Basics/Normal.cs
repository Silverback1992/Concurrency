namespace Concurrency.Basics;

public static class Normal
{
    public static async Task MainNormalAsync()
    {
        await SubNormalAsync();

        Console.WriteLine("Hi");

        Console.WriteLine("Some string");
    }

    private static async Task SubNormalAsync()
    {
        await Task.Delay(5000);

        Console.WriteLine("Some string 5 sec later");
    }
}
