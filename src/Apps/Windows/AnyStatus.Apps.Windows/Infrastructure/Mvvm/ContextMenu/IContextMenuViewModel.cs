using System.Collections.Generic;
using AnyStatus.Apps.Windows.Infrastructure.Mvvm.ContextMenu;

namespace AnyStatus.Apps.Windows.Features.ContextMenu;

public interface IContextMenuViewModel
{
    ICollection<IContextMenu> Items { get; set; }
    void                      Clear();
}