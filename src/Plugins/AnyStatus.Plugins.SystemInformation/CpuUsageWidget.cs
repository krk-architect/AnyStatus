using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AnyStatus.API.Widgets;

#pragma warning disable CA1416 // The call site is reachable on all platforms

namespace AnyStatus.Plugins.SystemInformation.OperatingSystem;

[Category("System Information")]
[DisplayName("CPU Usage")]
[Description("The total CPU usage (percentage)")]
public class CpuUsageWidget : MetricWidget, IPollable, ICommonWidget
{
    public CpuUsageWidget()
    {
        MinValue = 0;
        MaxValue = 100;
    }

    public override string ToString() => Value.ToString("0\\%");
}

public class CpuUsageQuery : AsyncMetricQuery<CpuUsageWidget>
{
    private const string InstanceName = "_Total";
    private const string CategoryName = "Processor";
    private const string CounterName  = "% Processor Time";

    protected override async Task Handle(MetricRequest<CpuUsageWidget> request, CancellationToken cancellationToken)
    {
        using PerformanceCounter counter = new (CategoryName, CounterName, InstanceName);

        counter.NextValue();

        await Task.Delay(500, cancellationToken).ConfigureAwait(false);

        request.Context.Value = Math.Round(counter.NextValue());

        request.Context.Status = Status.OK;
    }
}