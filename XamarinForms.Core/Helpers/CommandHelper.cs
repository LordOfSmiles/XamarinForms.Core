using Xamarin.CommunityToolkit.ObjectModel;

namespace XamarinForms.Core.Helpers;

public static class CommandHelper
{
    public static AsyncCommand<T> CreateAsyncCommand<T>(Func<T, Task> execute)
    {
        return new AsyncCommand<T>(execute, allowsMultipleExecutions: false);
    }

    public static AsyncCommand CreateAsyncCommand(Func<Task> execute)
    {
        return new AsyncCommand(execute, allowsMultipleExecutions: false);
    }
}