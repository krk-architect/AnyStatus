using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AnyStatus.API.Endpoints;
using AnyStatus.API.Services;
using AnyStatus.API.Widgets;
using AnyStatus.Plugins.UptimeRobot.API;

namespace AnyStatus.Plugins.UptimeRobot;

public class UptimeRobotMonitorsStatusCheck : AsyncStatusCheck<UptimeRobotMonitorsWidget>, IEndpointHandler<UptimeRobotEndpoint>
{
    private readonly IDispatcher _dispatcher;

    public UptimeRobotMonitorsStatusCheck(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public UptimeRobotEndpoint Endpoint { get; set; }

    protected override async Task Handle(StatusRequest<UptimeRobotMonitorsWidget> request, CancellationToken cancellationToken)
    {
        var response = await new UptimeRobotAPI(Endpoint).GetMonitorsAsync();

        if (response is null)
        {
            request.Context.Status = Status.None;
        }
        else
        {
            _dispatcher.Invoke(() =>
                               {
                                   var us = new UptimeRobotMonitorsSynchronizer(request.Context);
                                   us.Synchronize(response.Monitors.ToList(), request.Context.OfType<UptimeRobotReadOnlyMonitorWidget>().ToList());
                               });
        }
    }
}