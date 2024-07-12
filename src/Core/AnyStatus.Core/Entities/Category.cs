using System.Collections.Generic;
using System.Diagnostics;

namespace AnyStatus.Core.Widgets;

[DebuggerDisplay("{Name}")]
public class Category
{
    public string Name { get; set; }

    public List<Template> Templates { get; set; }
}