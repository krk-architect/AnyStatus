using System.Collections.Generic;

namespace AnyStatus.API.Widgets;

internal interface ITriggerActions
{
    ICollection<IActionTrigger> ActionTriggers { get; set; }
}