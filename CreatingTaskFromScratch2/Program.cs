using CreatingTaskFromScratch2;

Console.WriteLine($"Starting Thread Id: {Environment.CurrentManagedThreadId}");

DomeTrainTask.Run(() => Console.WriteLine($"First DomeTrainTask Id: {Environment.CurrentManagedThreadId} is running the task.")).Wait();

Console.WriteLine($"Second DomeTrainTask Id: {Environment.CurrentManagedThreadId} is running the task.");

DomeTrainTask.Run(() => Console.WriteLine($"Third DomeTrainTask Id: {Environment.CurrentManagedThreadId} is running the task.")).Wait();

//Console.ReadLine();
