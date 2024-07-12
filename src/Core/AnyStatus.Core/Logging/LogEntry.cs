using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace AnyStatus.Core.Logging;

[DebuggerDisplay("{LogLevel}   {Message}")]
public class LogEntry
{
    public DateTime Time { get; set; }

    public LogLevel LogLevel { get; set; }

    public string Message { get; set; }

    public int ThreadId { get; set; }

    public Exception Exception { get; set; }

    public bool HasException => Exception is not null;
}