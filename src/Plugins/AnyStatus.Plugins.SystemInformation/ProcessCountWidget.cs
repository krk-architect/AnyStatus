using System.ComponentModel;
using System.Diagnostics;
using AnyStatus.API.Widgets;
using MediatR;

#pragma warning disable CA1416 // The call site is reachable on all platforms

namespace AnyStatus.Plugins.SystemInformation.OperatingSystem;

[Category("System Information")]
[DisplayName("Process Count")]
[Description("The number of total processes")]
public class ProcessCountWidget : MetricWidget, IPollable, ICommonWidget
{
    public ProcessCountWidget() { Name = "Process Count"; }

    [Category("Process Count")]
    [DisplayName("Machine Name")]
    [Description("Optional. Leave blank for local computer.")]
    public string MachineName { get; set; }
}

public class ProcessCountQuery : RequestHandler<MetricRequest<ProcessCountWidget>>
{
    private const string CategoryName = "System";
    private const string CounterName  = "Processes";
    private const string InstanceName = "";

    protected override void Handle(MetricRequest<ProcessCountWidget> request)
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