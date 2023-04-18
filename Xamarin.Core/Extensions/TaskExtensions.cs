namespace Xamarin.Core.Extensions;

public static class TaskExtensions
{
    public static void FireAndForgetExample(Task taskToForget)
    {
        async Task Forget(Task task)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                // Log the exception.
            }
        }
        _ = Forget(taskToForget);
    }
}