using System.ComponentModel;
using System.Diagnostics;
using AnyStatus.API.Widgets;
using MediatR;

#pragma warning disable CA1416 // The call site is reachable on all platforms

namespace AnyStatus.Plugins.SystemInformation.OperatingSystem;

[Category("System Information")]
[DisplayName("Thread Count")]
[Description("The number of running CPU threads")]
public class ThreadCountWidget : MetricWidget, IPollable, ICommonWidget
{
    public ThreadCountWidget() { Name = "Thread Count"; }

    [Category("Thread Count")]
    [DisplayName("Machine Name")]
    [Description("Optional. Leave blank for local computer.")]
    public string MachineName { get; set; }
}

public class ThreadCountQuery : RequestHandler<MetricRequest<ThreadCountWidget>>
{
    private const string InstanceName = "";
    private const string CounterName  = "Threads";
    private const string CategoryName = "System";

    protected override void Handle(MetricRequest<ThreadCountWidget> request)
    {
        using PerformanceCounter counter = string.IsNullOrWhiteSpace(request.Context.MachineName)
                                               ? new (CategoryName
                                                    , CounterName)
                                               : new (CategoryName
                                                    , CounterName
                                                    , InstanceName
                                                    , request.Context.MachineName);

        request.Context.Value = (int)counter.NextValue();

        request.Context.Status = Status.OK;
    }
}