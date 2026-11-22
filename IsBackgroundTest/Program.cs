Console.WriteLine($"Main started on thread {Environment.CurrentManagedThreadId}");

var worker = new Thread(() =>
{
    Console.WriteLine("Worker: starting 5 seconds of work...");
    Thread.Sleep(5000);
    Console.WriteLine("Worker: done!");
});

worker.IsBackground = true;   // ← toggle this line

worker.Start();

Console.WriteLine("Main: reached the end. Goodbye!");
// No Console.ReadLine() on purpose