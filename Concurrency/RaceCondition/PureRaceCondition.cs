namespace Concurrency.RaceCondition;

public static class PureRaceCondition
{
    // The shared resource in the middle of the table
    private static int _sharedValue = 0;

    public static async Task RunTestAsync()
    {
        Console.WriteLine("Starting the Race Condition Sandbox...\n");

        // We run the race 10 times to watch the chaos
        for (int i = 1; i <= 10; i++)
        {
            _sharedValue = 0; // Reset the board

            // Thread A (Juggler 1)
            Task threadA = Task.Run(() =>
            {
                _sharedValue = 1;
            });

            // Thread B (Juggler 2)
            Task threadB = Task.Run(() =>
            {
                _sharedValue = 2;
            });

            // Wait for both threads to finish fighting
            await Task.WhenAll(threadA, threadB);

            // Print the survivor
            Console.WriteLine($"Race {i}: The final value is {_sharedValue}");
        }
    }
}
