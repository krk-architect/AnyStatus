using System;
using System.Collections.ObjectModel;
using AnyStatus.API.Services;
using AnyStatus.Apps.Windows.Infrastructure.Mvvm;
using AnyStatus.Core.Logging;

namespace AnyStatus.Apps.Windows.Features.Activity;

public sealed class ActivityViewModel : BaseViewModel, IDisposable
{
    private readonly IDispatcher _dispatcher;
    private readonly IDisposable _subscription;

    public ActivityViewModel(Logger logger, IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;

        _subscription = logger.LogEntries.Subscribe(logEntry => _dispatcher.Invoke(() => AddLogEntry(logEntry)));

        Commands.Add("Clear", new Command(_ => _dispatcher.Invoke(LogEntries.Clear), _ => LogEntries.Count > 0));
    }

    public ObservableCollection<LogEntry> LogEntries { get; } = new();

    public void Dispose() => _subscription.Dispose();

    private void AddLogEntry(LogEntry logEntry) { LogEntries.Add(logEntry); }
}