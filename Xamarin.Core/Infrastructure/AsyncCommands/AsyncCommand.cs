using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Xamarin.Core.Infrastructure.AsyncCommands
{
    public class AsyncCommand : IAsyncCommand
	{
		private readonly Func<Task> _execute;
		private readonly Func<object, bool>? _canExecute;
		private readonly Action<Exception>? _onException;
		private readonly bool _continueOnCapturedContext;
		private readonly WeakEventManager _weakEventManager = new WeakEventManager();

		/// <summary>
		/// Create a new AsyncCommand
		/// </summary>
		/// <param name="execute">Function to execute</param>
		/// <param name="canExecute">Function to call to determine if it can be executed</param>
		/// <param name="onException">Action callback when an exception occurs</param>
		/// <param name="continueOnCapturedContext">If the context should be captured on exception</param>
		public AsyncCommand(Func<Task> execute,
							Func<object, bool>? canExecute = null,
							Action<Exception>? onException = null,
							bool continueOnCapturedContext = false)
		{
			_execute = execute ?? throw new ArgumentNullException(nameof(execute));
			_canExecute = canExecute;
			_onException = onException;
			_continueOnCapturedContext = continueOnCapturedContext;
		}

		/// <summary>
		/// Event triggered when Can Excecute changes.
		/// </summary>
		public event EventHandler CanExecuteChanged
		{
			add => _weakEventManager.AddEventHandler(value);
			remove => _weakEventManager.RemoveEventHandler(value);
		}

		/// <summary>
		/// Invoke the CanExecute method and return if it can be executed.
		/// </summary>
		/// <param name="parameter">Parameter to pass to CanExecute.</param>
		/// <returns>If it can be executed.</returns>
		public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

		/// <summary>
		/// Execute the command async.
		/// </summary>
		/// <returns>Task of action being executed that can be awaited.</returns>
		public Task ExecuteAsync() => _execute();

		/// <summary>
		/// Raise a CanExecute change event.
		/// </summary>
		public void RaiseCanExecuteChanged() => _weakEventManager.HandleEvent(this, EventArgs.Empty, nameof(CanExecuteChanged));

		#region Explicit implementations
		void ICommand.Execute(object parameter) => ExecuteAsync().SafeFireAndForget(_onException, _continueOnCapturedContext);
		#endregion
	}
	/// <summary>
	/// Implementation of a generic Async Command
	/// </summary>
	public class AsyncCommand<T> : IAsyncCommand<T>
	{
		private readonly Func<T, Task> _execute;
		private readonly Func<object, bool>? _canExecute;
		private readonly Action<Exception>? _onException;
		private readonly bool _continueOnCapturedContext;
		private readonly WeakEventManager _weakEventManager = new WeakEventManager();

		/// <summary>
		/// Create a new AsyncCommand
		/// </summary>
		/// <param name="execute">Function to execute</param>
		/// <param name="canExecute">Function to call to determine if it can be executed</param>
		/// <param name="onException">Action callback when an exception occurs</param>
		/// <param name="continueOnCapturedContext">If the context should be captured on exception</param>
		public AsyncCommand(Func<T, Task> execute,
							Func<object, bool>? canExecute = null,
							Action<Exception>? onException = null,
							bool continueOnCapturedContext = false)
		{
			_execute = execute ?? throw new ArgumentNullException(nameof(execute));
			_canExecute = canExecute;
			_onException = onException;
			_continueOnCapturedContext = continueOnCapturedContext;
		}

		/// <summary>
		/// Event triggered when Can Excecute changes.
		/// </summary>
		public event EventHandler CanExecuteChanged
		{
			add => _weakEventManager.AddEventHandler(value);
			remove => _weakEventManager.RemoveEventHandler(value);
		}

		/// <summary>
		/// Invoke the CanExecute method and return if it can be executed.
		/// </summary>
		/// <param name="parameter">Parameter to pass to CanExecute.</param>
		/// <returns>If it can be executed</returns>
		public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

		/// <summary>
		/// Execute the command async.
		/// </summary>
		/// <returns>Task that is executing and can be awaited.</returns>
		public Task ExecuteAsync(T parameter) => _execute(parameter);

		/// <summary>
		/// Raise a CanExecute change event.
		/// </summary>
		public void RaiseCanExecuteChanged() => _weakEventManager.HandleEvent(this, EventArgs.Empty, nameof(CanExecuteChanged));

		#region Explicit implementations

		void ICommand.Execute(object parameter)
		{
			if (CommandUtils.IsValidCommandParameter<T>(parameter))
				ExecuteAsync((T)parameter).SafeFireAndForget(_onException, _continueOnCapturedContext);

		}
		#endregion
	}
	
	internal static class CommandUtils
	{
		internal static bool IsValidCommandParameter<T>(object o)
		{
			bool valid;
			if (o != null)
			{
				// The parameter isn't null, so we don't have to worry whether null is a valid option
				valid = o is T;

				if (!valid)
					throw new InvalidCommandParameterException(typeof(T), o.GetType());

				return valid;
			}

			var t = typeof(T);

			// The parameter is null. Is T Nullable?
			if (Nullable.GetUnderlyingType(t) != null)
			{
				return true;
			}

			// Not a Nullable, if it's a value type then null is not valid
			valid = !t.GetTypeInfo().IsValueType;

			if (!valid)
				throw new InvalidCommandParameterException(typeof(T));

			return valid;
		}
	}
	
	public class InvalidCommandParameterException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MvvmHelpersInvalidCommandParameterException"/> class.
		/// </summary>
		/// <param name="expectedType">Expected parameter type for AsyncCommand.Execute.</param>
		/// <param name="actualType">Actual parameter type for AsyncCommand.Execute.</param>
		/// <param name="innerException">Inner Exception</param>
		public InvalidCommandParameterException(Type expectedType, Type actualType, Exception innerException) : base(CreateErrorMessage(expectedType, actualType), innerException)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:TaskExtensions.MVVM.InvalidCommandParameterException"/> class.
		/// </summary>
		/// <param name="expectedType">Expected parameter type for AsyncCommand.Execute.</param>
		/// <param name="innerException">Inner Exception</param>
		public InvalidCommandParameterException(Type expectedType, Exception innerException) : base(CreateErrorMessage(expectedType), innerException)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:MvvmHelpers.InvalidCommandParameterException"/> class.
		/// </summary>
		/// <param name="expectedType">Expected parameter type for AsyncCommand.Execute.</param>
		/// <param name="actualType">Actual parameter type for AsyncCommand.Execute.</param>
		public InvalidCommandParameterException(Type expectedType, Type actualType) : base(CreateErrorMessage(expectedType, actualType))
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:TaskExtensions.MVVM.InvalidCommandParameterException"/> class.
		/// </summary>
		/// <param name="expectedType">Expected parameter type for AsyncCommand.Execute.</param>
		public InvalidCommandParameterException(Type expectedType) : base(CreateErrorMessage(expectedType))
		{

		}

		private static string CreateErrorMessage(Type expectedType, Type actualType) =>
			$"Invalid type for parameter. Expected Type: {expectedType}, but received Type: {actualType}";

		private static string CreateErrorMessage(Type expectedType) =>
			$"Invalid type for parameter. Expected Type {expectedType}";
	}
	
	public interface IAsyncCommand : ICommand
	{
		/// <summary>
		/// Execute the command async.
		/// </summary>
		/// <returns>Task to be awaited on.</returns>
		Task ExecuteAsync();
	}

	/// <summary>
	/// Interface for Async Command with parameter
	/// </summary>
	public interface IAsyncCommand<T> : ICommand
	{
		/// <summary>
		/// Execute the command async.
		/// </summary>
		/// <param name="parameter">Parameter to pass to command</param>
		/// <returns>Task to be awaited on.</returns>
		Task ExecuteAsync(T parameter);
	}

}