using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace AnyStatus.Core.Logging;

public class Logger : ILogger, IDisposable
{
    private const int BufferSize = 100;

    private readonly ReplaySubject<LogEntry> _buffer = new(BufferSize);

    public IObservable<LogEntry> LogEntries => _buffer.AsObservable();

    public IDisposable BeginScope<TState>(TState state) => null;

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        _buffer.OnNext(new()
                       {
                           Time      = DateTime.Now
                         , LogLevel  = logLevel
                         , Exception = exception
                         , Message   = formatter(state, exception)
                         , ThreadId  = Thread.CurrentThread.ManagedThreadId
                       });
    }

    #region IDisposable

    private bool IsDisposed { get; set; }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (IsDisposed)
        {
            return;
        }

        if (disposing)
        {
            // Dispose managed resources.
            _buffer.Dispose();
        }

        // Dispose unmanaged resources here, if any.

        IsDisposed = true;
    }

    #endregion
}