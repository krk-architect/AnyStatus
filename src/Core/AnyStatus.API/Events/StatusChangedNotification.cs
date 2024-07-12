using System;
using System.Diagnostics;
using AnyStatus.API.Widgets;
using MediatR;

namespace AnyStatus.API.Events;

public class StatusChangedNotification
{
    public static INotification Create(object widget)
    {
        var type = typeof(StatusChangedNotification<>).MakeGenericType(widget.GetType());

        return Activator.CreateInstance(type, widget) as INotification;
    }
}

[DebuggerDisplay("{Widget}")]
public class StatusChangedNotification<TWidget> : INotification
    where TWidget : class, IWidget
{
    public StatusChangedNotification(TWidget widget)
    {
        Widget = widget ?? throw new ArgumentNullException(nameof(widget));
    }

    public TWidget Widget { get; }
}