using Concurrency;
using Concurrency.Basics;
using Concurrency.Cancellation;
using Concurrency.RaceCondition;
using Concurrency.ValueTask;
using System.Diagnostics;

//var normalWatch = Stopwatch.StartNew();
//await MakeBreakfastNormal.MakeBreakfastNormalAsync();
//normalWatch.Stop();
//Console.WriteLine($"Normal Breakfast Total Time: {normalWatch.Elapsed.TotalSeconds:F2} seconds\n");

//var fastWatch = Stopwatch.StartNew();
//await MakeBreakfastFast.MakeBreakfastFastAsync();
//fastWatch.Stop();
//Console.WriteLine($"Fast Breakfast Total Time: {fastWatch.Elapsed.TotalSeconds:F2} seconds\n");

await PureRaceCondition.RunTestAsync();
