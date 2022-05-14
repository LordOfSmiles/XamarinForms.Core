using System.Diagnostics;
using System.Text;

namespace XamarinForms.Core.Helpers.TaskMonitor;

public abstract class TaskMonitorBase : ITaskMonitor
{
    /// <summary>
    /// Instance logger.
    /// </summary>
    protected readonly Action<ITaskMonitor, string, Exception> ErrorHandler;

    /// <summary>
    /// If true we monitor the task in the constructor to start it.
    /// </summary>
    private readonly bool _isHot;

    /// <summary>
    /// If true wrap the task in a new Task.
    /// </summary>
    private readonly bool _inNewTask;

    private readonly bool? _considerCanceledAsFaulted;

    /// <summary>
    /// Callback called when the task has been canceled.
    /// </summary>
    private readonly Action<ITaskMonitor> _whenCanceled;

    /// <summary>
    /// Callback called when the task is faulted.
    /// </summary>
    private readonly Action<ITaskMonitor> _whenFaulted;

    /// <summary>
    /// Callback called when the task completed (successfully or not).
    /// </summary>
    private readonly Action<ITaskMonitor> _whenCompleted;

    private bool _areCallbacksCancelled;

    /// <summary>
    /// Initializes a task notifier watching the specified task.
    /// </summary>
    protected TaskMonitorBase(
        Task task,
        Action<ITaskMonitor> whenCanceled = null,
        Action<ITaskMonitor> whenFaulted = null,
        Action<ITaskMonitor> whenCompleted = null,
        string name = null,
        bool inNewTask = false,
        bool isHot = false,
        bool? considerCanceledAsFaulted = null,
        Action<ITaskMonitor, string, Exception> errorHandler = null)
    {
        Task = task;
        _whenCanceled = whenCanceled;
        _whenFaulted = whenFaulted;
        _whenCompleted = whenCompleted;
        _inNewTask = inNewTask;
        _isHot = isHot;
        _considerCanceledAsFaulted = considerCanceledAsFaulted;
        Name = name;
        ErrorHandler = errorHandler ?? TaskMonitorConfiguration.ErrorHandler;
    }

    /// <inheritdoc />
    public Task Task { get; }

    /// <inheritdoc />
    public Task TaskCompleted { get; protected set; }

    /// <inheritdoc />
    public TaskStatus Status => Task.Status;

    /// <inheritdoc />
    public bool IsCompleted => Task.IsCompleted;

    /// <inheritdoc />
    public bool IsNotStarted => Task.Status == TaskStatus.Created;

    /// <inheritdoc />
    public bool IsNotCompleted => !Task.IsCompleted;

    /// <inheritdoc />
    public bool IsSuccessfullyCompleted => Task.Status == TaskStatus.RanToCompletion;

    /// <inheritdoc />
    public bool IsCanceled => Task.IsCanceled;

    /// <inheritdoc />
    public bool IsFaulted => Task.IsFaulted || (ConsiderCanceledAsFaulted && Task.IsCanceled);

    /// <inheritdoc />
    public string Name { get; }

    public bool ConsiderCanceledAsFaulted =>
        (_considerCanceledAsFaulted.HasValue && _considerCanceledAsFaulted.Value)
        || TaskMonitorConfiguration.ConsiderCanceledAsFaulted;

    public bool HasName => Name != null;

    /// <inheritdoc />
    public AggregateException Exception => Task.Exception;

    /// <inheritdoc />
    public Exception InnerException => Exception?.InnerException;

    /// <inheritdoc />
    public string ErrorMessage => InnerException?.Message;

    protected virtual bool HasCallbacks => _whenCanceled != null || _whenCompleted != null || _whenFaulted != null;

    /// <inheritdoc />
    public void Start()
    {
        if (!_isHot)
        {
            TaskCompleted = MonitorTaskAsync(Task);
        }
    }

    public void CancelCallbacks()
    {
        _areCallbacksCancelled = true;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        if (HasName)
        {
            builder.Append(Name);
            builder.Append(" => ");
        }

        builder.Append("Status: ");
        if (IsNotStarted)
        {
            builder.Append(nameof(IsNotStarted));
        }
        else if (IsNotCompleted)
        {
            builder.Append(nameof(IsNotCompleted));
        }
        else if (IsSuccessfullyCompleted)
        {
            builder.Append(nameof(IsSuccessfullyCompleted));
        }
        else if (IsCanceled)
        {
            builder.Append(nameof(IsCanceled));
        }
        else
        {
            builder.Append(nameof(IsFaulted));
        }

        return builder.ToString();
    }

    protected async Task MonitorTaskAsync(Task task)
    {
        Stopwatch stopWatch = null;
        if (TaskMonitorConfiguration.LogStatistics)
        {
            stopWatch = new Stopwatch();
            stopWatch.Start();
        }

        try
        {
            if (_inNewTask)
            {
                await Task.Run(async () => await task);
            }
            else
            {
                await task;
            }
        }
        catch (TaskCanceledException canceledException)
        {
            ErrorHandler?.Invoke(this, "Task has been canceled", canceledException);
        }
        catch (Exception exception)
        {
            ErrorHandler?.Invoke(this, "Error in wrapped task", exception);
        }
        finally
        {
            if (stopWatch != null)
            {
                stopWatch.Stop();
                TaskMonitorConfiguration.StatisticsHandler?.Invoke(this, stopWatch.Elapsed);
            }

            OnTaskCompleted();
        }
    }

    protected abstract void OnSuccessfullyCompleted();

    private void OnTaskCompleted()
    {
        if (_areCallbacksCancelled || !HasCallbacks)
        {
            return;
        }

        try
        {
            _whenCompleted?.Invoke(this);
        }
        catch (Exception exception)
        {
            ErrorHandler?.Invoke(this, "Error while calling the WhenCompleted callback", exception);
        }

        if (IsCanceled && !ConsiderCanceledAsFaulted)
        {
            try
            {
                _whenCanceled?.Invoke(this);
            }
            catch (Exception exception)
            {
                ErrorHandler?.Invoke(this, "Error while calling the WhenCanceled callback", exception);
            }
        }
        else if (IsFaulted)
        {
            try
            {
                _whenFaulted?.Invoke(this);
            }
            catch (Exception exception)
            {
                ErrorHandler?.Invoke(this, "Error while calling the WhenFaulted callback", exception);
            }
        }
        else
        {
            OnSuccessfullyCompleted();
        }
    }
}