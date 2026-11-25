using CreatingTaskFromScratch3;

Console.WriteLine($"Starting Thread Id: {Environment.CurrentManagedThreadId}");

await DomeTrainTask.Run(() => Console.WriteLine($"First DomeTrainTask Id: {Environment.CurrentManagedThreadId} is running the task."));

await DomeTrainTask.Delay(TimeSpan.FromSeconds(1));

Console.WriteLine($"Second DomeTrainTask Id: {Environment.CurrentManagedThreadId} is running the task.");

await DomeTrainTask.Delay(TimeSpan.FromSeconds(1));

await DomeTrainTask.Run(() => Console.WriteLine($"Third DomeTrainTask Id: {Environment.CurrentManagedThreadId} is running the task."));
