using System;
using System.Collections.Generic;
using System.Text;

namespace CreatingTaskFromScratch;

public class DomeTrainTask
{
    private readonly Lock _lock = new();
    private bool _completed;
    private Exception? _exception;

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

    public void SetResult()
    {
        lock (_lock)
        {
            if (_completed)
            {
                throw new InvalidOperationException("Task is already completed.");
            }

            _completed = true;
        }
    }

    public void SetException(Exception exception)
    {
        lock (_lock)
        {
            if (_completed)
            {
                throw new InvalidOperationException("Task is already completed.");
            }

            _exception = exception;
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
}
