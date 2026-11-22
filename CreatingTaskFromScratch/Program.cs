using CreatingTaskFromScratch;

Console.WriteLine($"Current Thread Id: {Thread.CurrentThread.ManagedThreadId}");

DomeTrainTask.Run(() =>
{
    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is running the task.");
});

Console.ReadLine();
