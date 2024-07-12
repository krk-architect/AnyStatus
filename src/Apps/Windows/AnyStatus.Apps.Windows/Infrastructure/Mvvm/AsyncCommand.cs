using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AnyStatus.Apps.Windows.Infrastructure.Mvvm;

public class AsyncCommand : ICommand
{
    private readonly Func<object, bool> _canExecute;
    private readonly Func<object, Task> _execute;
    private          bool               _isExecuting;

    public AsyncCommand(Func<object, Task> execute, Func<object, bool> canExecute = null)
    {
        _execute    = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object parameter) => !_isExecuting && (_canExecute?.Invoke(parameter) ?? true);

    public void Execute(object parameter)
    {
        ExecuteAsync(parameter).FireAndForgetSafeAsync();

        var task = ExecuteAsync(parameter);
        task.FireAndForgetSafeAsync();
    }

    private async Task ExecuteAsync(object parameter)
    {
        if (CanExecute(parameter))
        {
            _isExecuting = true;
            RaiseCanExecuteChanged();

            try
            {
                await _execute(parameter).ConfigureAwait(false);
            }
            finally
            {
                _isExecuting = false;
                RaiseCanExecuteChanged();
            }
        }
    }

    public static void RaiseCanExecuteChanged() => CommandManager.InvalidateRequerySuggested();
}

public static class TaskExtensions
{
    // ReSharper disable once AsyncVoidMethod
    // ReSharper disable once AvoidAsyncVoid
    [SuppressMessage("AsyncUsage", "AsyncFixer03:Fire-and-forget async-void methods or delegates", Justification = "Fire and Forget should return void instead of Task")]
    public static async void FireAndForgetSafeAsync(this Task task, Action<Exception> handleException = null)
    {
        try
        {
            await task.ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            handleException?.Invoke(ex);
        }
    }
}