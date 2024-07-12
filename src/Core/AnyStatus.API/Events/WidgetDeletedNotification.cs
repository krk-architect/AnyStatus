﻿using System;
using System.Diagnostics;
using AnyStatus.API.Widgets;
using MediatR;

namespace AnyStatus.API.Events;

[DebuggerDisplay("{Widget}")]
public class WidgetDeletedNotification : INotification
{
    public WidgetDeletedNotification(IWidget widget)
    {
        Widget = widget ?? throw new ArgumentNullException(nameof(widget));
    }

    public IWidget Widget { get; }
}