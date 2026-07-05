namespace Concurrency.Basics;

public static class MakeBreakfastFast
{
    public static async Task MakeBreakfastFastAsync()
    {
        // 1. Fire and store! We drop bread in the toaster. 
        // We get a Task handle back immediately. We DO NOT await yet!
        Task<Toast> toastTask = ToastBreadAsync();

        // 2. While the toaster is heating up in the background, our execution flow CONTINUES!
        // We immediately chop onions, grease the pan, and fry the eggs concurrently.
        Egg eggs = await FryEggsAsync();

        // 3. Now our eggs are ready on the plate. We finally turn our attention back to the toaster.
        // If the toast finished while we were frying eggs, this await returns instantly (0ms)!
        Toast toast = await toastTask;

        // Total time elapsed: ~3 seconds. Hot toast, hot eggs!
    }

    private static async Task<Egg> FryEggsAsync()
    {
        await Task.Delay(3000);
        return new Egg();
    }

    private static async Task<Toast> ToastBreadAsync()
    {
        await Task.Delay(3000);
        return new Toast();
    }
}
