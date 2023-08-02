using CommunityToolkit.Mvvm.Input;

namespace XamarinForms.Core.Helpers;

public static class CommandHelper
{
    public static IRelayCommand<T> Create<T>(Action<T> execute, Predicate<T> canExecute)
    {
        return new RelayCommand<T>(execute, canExecute);
    }

    public static IRelayCommand<T> Create<T>(Action<T> execute)
    {
        return new RelayCommand<T>(execute);
    }

    public static IRelayCommand Create(Action execute, Func<bool> canExecute)
    {
        return new RelayCommand(execute, canExecute);
    }

    public static IRelayCommand Create(Action execute)
    {
        return new RelayCommand(execute);
    }

    public static IAsyncRelayCommand<T> Create<T>(Func<T, Task> execute, Predicate<T> canExecute)
    {
        return new AsyncRelayCommand<T>(execute, canExecute);
    }

    public static IAsyncRelayCommand<T> Create<T>(Func<T, Task> execute)
    {
        return new AsyncRelayCommand<T>(execute);
    }

    public static IAsyncRelayCommand Create(Func<Task> execute, Func<bool> canExecute)
    {
        return new AsyncRelayCommand(execute, canExecute);
    }

    public static IAsyncRelayCommand Create(Func<Task> execute)
    {
        return new AsyncRelayCommand(execute);
    }
}