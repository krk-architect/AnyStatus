using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AnyStatus.API.Endpoints;
using AnyStatus.API.Services;
using AnyStatus.API.Widgets;
using Docker.DotNet.Models;

namespace AnyStatus.Plugins.Docker.Containers;

public class DockerContainersHealthCheck : AsyncStatusCheck<DockerContainersWidget>, IEndpointHandler<DockerEndpoint>
{
    private readonly IDispatcher _dispatcher;

    public DockerContainersHealthCheck(IDispatcher dispatcher) { _dispatcher = dispatcher; }

    public DockerEndpoint Endpoint { get; set; }

    protected override async Task Handle(StatusRequest<DockerContainersWidget> request, CancellationToken cancellationToken)
    {
        ICollection<ContainerListResponse> containers;

        using (var client = Endpoint.GetClient())
        {
            containers = await client.Containers.ListContainersAsync(new()
                                                                     {
                                                                         All = true
                                                                     })
                                     .ConfigureAwait(false);
        }

        if (containers is null)
        {
            request.Context.Status = Status.Unknown;
        }
        else if (containers.Count > 0)
        {
            _dispatcher.Invoke(() =>
                               {
                                   var cs = new DockerContainersSynchronizer(request.Context);
                                   cs.Synchronize(containers
                                                , request.Context
                                                         .OfType<ReadOnlyDockerContainerWidget>()
                                                         .ToList());
                               });
        }
        else
        {
            request.Context.Status = Status.OK;
        }
    }
}