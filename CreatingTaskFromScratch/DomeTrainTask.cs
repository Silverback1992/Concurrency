namespace CreatingTaskFromScratch;

public class DomeTrainTask
{
    private readonly Lock _lock = new();
    private bool _completed;
    private Exception? _exception;
    private Action? _action;
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

            if (_action == null)
            {
                return;
            }

            if (_context == null)
            {
                _action.Invoke();
                return;
            }

            ExecutionContext.Run(_context, state => ((Action?)state)?.Invoke(), _action);

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
                _action = action;
                _context = ExecutionContext.Capture();
            }
        }

        return continuationTask;
    }
}
