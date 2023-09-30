using CommunityToolkit.Mvvm.Input;

namespace XamarinForms.Core.Helpers;

public static class CommandHelper
{
    public static IRelayCommand<T> Create<T>(Action<T> execute, Predicate<T> canExecute) => new RelayCommand<T>(execute, canExecute);

    public static IRelayCommand<T> Create<T>(Action<T> execute) => new RelayCommand<T>(execute);

    public static IRelayCommand Create(Action execute, Func<bool> canExecute) => new RelayCommand(execute, canExecute);

    public static IRelayCommand Create(Action execute) => new RelayCommand(execute);

    public static IAsyncRelayCommand<T> Create<T>(Func<T, Task> execute, Predicate<T> canExecute) => new AsyncRelayCommand<T>(execute, canExecute);

    public static IAsyncRelayCommand<T> Create<T>(Func<T, Task> execute) => new AsyncRelayCommand<T>(execute);

    public static IAsyncRelayCommand Create(Func<Task> execute, Func<bool> canExecute) => new AsyncRelayCommand(execute, canExecute);

    public static IAsyncRelayCommand Create(Func<Task> execute) => new AsyncRelayCommand(execute);
}