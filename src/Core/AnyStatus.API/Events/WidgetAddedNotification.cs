using AnyStatus.API.Widgets;
using MediatR;
using System;
using System.Diagnostics;

namespace AnyStatus.API.Events
{
    [DebuggerDisplay("{Widget}")]
    public class WidgetAddedNotification : INotification
    {
        public WidgetAddedNotification(IWidget widget)
        {
            Widget = widget ?? throw new ArgumentNullException(nameof(widget));
        }

        public IWidget Widget { get; }
    }
}
