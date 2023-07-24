using CommunityToolkit.Mvvm.Input;

namespace XamarinForms.Core.Helpers;

public static class CommandHelper
{
    public static IRelayCommand<T> CreateCommand<T>(Action<T> execute, Predicate<T> canExecute)
    {
        return new RelayCommand<T>(execute, canExecute);
    }

    public static IRelayCommand<T> CreateCommand<T>(Action<T> execute)
    {
        return new RelayCommand<T>(execute);
    }

    public static IRelayCommand CreateCommand(Action execute, Func<bool> canExecute)
    {
        return new RelayCommand(execute, canExecute);
    }

    public static IRelayCommand CreateCommand(Action execute)
    {
        return new RelayCommand(execute);
    }

    public static IAsyncRelayCommand<T> CreateAsyncCommand<T>(Func<T, Task> execute, Predicate<T> canExecute)
    {
        return new AsyncRelayCommand<T>(execute, canExecute);
    }

    public static IAsyncRelayCommand<T> CreateAsyncCommand<T>(Func<T, Task> execute)
    {
        return new AsyncRelayCommand<T>(execute);
    }

    public static IAsyncRelayCommand CreateAsyncCommand(Func<Task> execute, Func<bool> canExecute)
    {
        return new AsyncRelayCommand(execute, canExecute);
    }

    public static IAsyncRelayCommand CreateAsyncCommand(Func<Task> execute)
    {
        return new AsyncRelayCommand(execute);
    }
}