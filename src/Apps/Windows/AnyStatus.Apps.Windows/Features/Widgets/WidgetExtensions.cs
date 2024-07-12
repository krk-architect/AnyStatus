using System;
using AnyStatus.API.Widgets;

namespace AnyStatus.Apps.Windows.Features.Widgets;

public static class WidgetExtensions
{
    public static void Collapse(this IWidget widget, bool includeChildren = false)
    {
        ArgumentNullException.ThrowIfNull(widget);

        widget.IsExpanded = false;

        if (!includeChildren || !widget.HasChildren)
        {
            return;
        }

        foreach (var child in widget)
        {
            child.Collapse(true);
        }
    }

    public static void Expand(this IWidget widget, bool includeChildren = false)
    {
        ArgumentNullException.ThrowIfNull(widget);

        widget.IsExpanded = true;

        if (!includeChildren || !widget.HasChildren)
        {
            return;
        }

        foreach (var child in widget)
        {
            child.Expand(true);
        }
    }

    public static void Remove(this IWidget widget)
    {
        ArgumentNullException.ThrowIfNull(widget);

        if (widget.Parent is null)
        {
            throw new InvalidOperationException("Parent not found.");
        }

        if (!widget.Parent.Contains(widget))
        {
            return;
        }

        widget.Parent.Remove(widget);

        widget.Parent = null;
    }

    public static void MoveUp(this IWidget item)
    {
        ArgumentNullException.ThrowIfNull(item);

        if (!item.CanMoveUp())
        {
            return;
        }

        var index = item.Parent.IndexOf(item);

        item.Parent.Move(index, index - 1);
    }

    public static bool CanMoveUp(this IWidget item) => item is IMovable
                                                    && item.Parent               != null
                                                    && item.Parent.IndexOf(item) > 0;

    public static void MoveDown(this IWidget widget)
    {
        ArgumentNullException.ThrowIfNull(widget);

        if (!widget.CanMoveDown())
        {
            return;
        }

        var index = widget.Parent.IndexOf(widget);

        widget.Parent.Move(index, index + 1);
    }

    public static bool CanMoveDown(this IWidget item) => item is IMovable
                                                      && item.Parent                   != null
                                                      && item.Parent.IndexOf(item) + 1 < item.Parent.Count;
}