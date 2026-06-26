namespace Concurrency.ParallelProgramming;

public static class ParallelExampke
{
    public static void Run()
    {
        int[] numbers = [.. Enumerable.Range(1, 100000)];
        int primeCount = 0;
        Parallel.ForEach(numbers, number =>
        {
            if (PrimeChecker.IsPrime(number))
            {
                Interlocked.Increment(ref primeCount);
            }
        });
        Console.WriteLine($"Total prime numbers between 1 and 100000: {primeCount}");
    }

    public static void RunWithPLINQ()
    {
        int[] numbers = [.. Enumerable.Range(1, 100000)];
        int primeCount = numbers.AsParallel().Count(PrimeChecker.IsPrime);
        Console.WriteLine($"Total prime numbers between 1 and 100000 using PLINQ: {primeCount}");
    }
}
