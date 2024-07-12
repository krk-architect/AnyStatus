using System.Diagnostics;
using MediatR;

namespace AnyStatus.Core.App;

[DebuggerDisplay("{Context}")]
public class ContextLoaded : INotification
{
    public ContextLoaded(IAppContext context) { Context = context; }

    public IAppContext Context { get; }
}