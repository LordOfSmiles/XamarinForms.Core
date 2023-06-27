namespace Xamarin.Core.Extensions;

public static class TaskExtensions
{
    public static void FireAndForget(this Task task,
                                     Action<Exception> errorHandler = null)
    {
        task.ContinueWith(t =>
        {
            if (t.IsFaulted && errorHandler != null)
                errorHandler(t.Exception);
        }, TaskContinuationOptions.OnlyOnFaulted);
    }
    
    public static async Task<TResult> Retry<TResult>(this Func<Task<TResult>> taskFactory, int maxRetries, TimeSpan delay)
    {
        for (int i = 0; i < maxRetries; i++)
        {
            try
            {
                return await taskFactory().ConfigureAwait(false);
            }
            catch
            {
                if (i == maxRetries - 1)
                    throw;
                await Task.Delay(delay).ConfigureAwait(false);
            }
        }

        return default(TResult); // Should not be reached
    }
    
    public static async Task OnFailure(this Task task, Action<Exception> onFailure)
    {
        try
        {
            await task.ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            onFailure(ex);
        }
    }
}