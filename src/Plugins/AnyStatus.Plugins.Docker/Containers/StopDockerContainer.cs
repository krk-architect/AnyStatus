using System.Threading;
using System.Threading.Tasks;
using AnyStatus.API.Endpoints;
using AnyStatus.API.Widgets;

namespace AnyStatus.Plugins.Docker.Containers;

public class StopDockerContainer : AsyncStopRequestHandler<ReadOnlyDockerContainerWidget>, IEndpointHandler<DockerEndpoint>
{
    public DockerEndpoint Endpoint { get; set; }

    protected override async Task Handle(StopRequest<ReadOnlyDockerContainerWidget> request, CancellationToken cancellationToken)
    {
        using var client = Endpoint.GetClient();

        await client.Containers.StopContainerAsync(request.Context.ContainerId, new (), cancellationToken);
    }
}