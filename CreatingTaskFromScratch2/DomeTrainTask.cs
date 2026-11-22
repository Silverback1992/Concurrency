using System.Runtime.ExceptionServices;

namespace CreatingTaskFromScratch2;

public class DomeTrainTask
{
    private readonly Lock _lock = new();
    private bool _completed;
    private Exception? _exception;
    private Action? _nextAction;
    private ExecutionContext? _context;

    public bool IsCompleted
    {
        get
        {
            lock (_lock)
            {
                return _completed;
            }
        }
    }

    public void SetResult() => CompleteTask(null);

    public void SetException(Exception exception) => CompleteTask(exception);

    private void CompleteTask(Exception? exception)
    {
        lock (_lock)
        {
            if (_completed)
            {
                throw new InvalidOperationException("Task is already completed.");
            }

            _completed = true;
            _exception = exception;

            if (_nextAction == null)
            {
                return;
            }

            if (_context == null)
            {
                _nextAction.Invoke();
                return;
            }

            ExecutionContext.Run(_context, state => ((Action?)state)?.Invoke(), _nextAction);
        }
    }

    public static DomeTrainTask Run(Action action)
    {
        var task = new DomeTrainTask();

        ThreadPool.QueueUserWorkItem(_ =>
        {
            try
            {
                action();
                task.SetResult();
            }
            catch (Exception ex)
            {
                task.SetException(ex);
            }
        });

        return task;
    }

    public DomeTrainTask ContinueWith(Action action)
    {
        var continuationTask = new DomeTrainTask();

        lock (_lock)
        {
            if (_completed)
            {
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    try
                    {
                        action();
                        continuationTask.SetResult();
                    }
                    catch (Exception ex)
                    {
                        continuationTask.SetException(ex);
                    }
                });
            }
            else
            {
                _nextAction = action;
                _context = ExecutionContext.Capture();
            }
        }

        return continuationTask;
    }

    public void Wait()
    {
        ManualResetEventSlim? resetEventSlim = null;

        lock (_lock)
        {
            if (!_completed)
            {
                resetEventSlim = new ManualResetEventSlim();
                ContinueWith(() => resetEventSlim.Set());
            }
        }

        resetEventSlim?.Wait();

        if (_exception != null)
        {
            ExceptionDispatchInfo.Throw(_exception);
        }
    }
}
