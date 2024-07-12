using System.ComponentModel;
using System.Diagnostics;
using AnyStatus.API.Widgets;
using MediatR;

#pragma warning disable CA1416 // The call site is reachable on all platforms

namespace AnyStatus.Plugins.SystemInformation.OperatingSystem;

[Category("System Information")]
[DisplayName("Page File Usage")]
[Description("Shows the percentage of page file usage")]
public class PageFileUsageWidget : MetricWidget, IPollable, ICommonWidget
{
    public PageFileUsageWidget()
    {
        MinValue = 0;
        MaxValue = 100;
        Name     = "Page File Usage";
    }

    [Category("Page File Usage")]
    [DisplayName("Machine Name")]
    [Description("Optional. Leave blank for local computer.")]
    public string MachineName { get; set; }

    public override string ToString() => Value.ToString("0\\%");
}

public class PageFileUsageQuery : RequestHandler<MetricRequest<PageFileUsageWidget>>
{
    private const string CategoryName = "Paging File";
    private const string CounterName  = "% Usage";
    private const string InstanceName = "_Total";

    protected override void Handle(MetricRequest<PageFileUsageWidget> request)
    {
        using PerformanceCounter counter = string.IsNullOrWhiteSpace(request.Context.MachineName)
                                               ? new (CategoryName
                                                    , CounterName
                                                    , InstanceName)
                                               : new (CategoryName
                                                    , CounterName
                                                    , InstanceName
                                                    , request.Context.MachineName);

        request.Context.Value = (int)counter.NextValue();

        request.Context.Status = Status.OK;
    }
}