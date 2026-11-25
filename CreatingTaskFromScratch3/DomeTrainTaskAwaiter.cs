using System.Runtime.CompilerServices;

namespace CreatingTaskFromScratch3;

public readonly struct DomeTrainTaskAwaiter : INotifyCompletion
{
    private readonly DomeTrainTask _task;

    public DomeTrainTaskAwaiter(DomeTrainTask task)
    {
        _task = task;
    }

    public bool IsCompleted => _task.IsCompleted;

    public DomeTrainTaskAwaiter GetAwaiter() => this;

    public void GetResult() => _task.Wait();

    public void OnCompleted(Action continuation)
    {
        _task.ContinueWith(continuation);
    }
}
