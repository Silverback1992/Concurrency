namespace Concurrency.Basics;

public static class MakeBreakfastNormal
{
    public static async Task MakeBreakfastNormalAsync()
    {
        // We start toasting and IMMEDIATELY wait 3 seconds doing nothing else.
        await ToastBreadAsync();

        // Only AFTER the toast is done do we even start the stove for eggs.
        await FryEggsAsync();

        // Total time elapsed: 6 seconds. Cold toast!
    }

    private static async Task ToastBreadAsync()
    {
        await Task.Delay(3000);
    }

    private static async Task FryEggsAsync()
    {
        await Task.Delay(3000);
    }
}
