using CreatingTaskFromScratch;

Console.WriteLine($"Starting Thread Id: {Environment.CurrentManagedThreadId}");

var task = DomeTrainTask.Run(() =>
{
    Console.WriteLine($"First DomeTrainTask Id: {Environment.CurrentManagedThreadId} is running the task.");
});

task.ContinueWith(() =>
{
    DomeTrainTask.Run(() =>
    {
        Console.WriteLine($"Third DomeTrainTask Id: {Environment.CurrentManagedThreadId} is running the task.");
    });

    Console.WriteLine($"Second DomeTrainTask Id: {Environment.CurrentManagedThreadId} is running the task.");
});

Console.ReadLine();
