using System.Threading;
using System.Threading.Tasks;
using AnyStatus.API.Endpoints;
using AnyStatus.API.Widgets;

namespace AnyStatus.Plugins.Docker.Containers;

public class RemoveDockerContainer : AsyncRemoveRequestHandler<ReadOnlyDockerContainerWidget>, IEndpointHandler<DockerEndpoint>
{
    public DockerEndpoint Endpoint { get; set; }

    protected override async Task Handle(RemoveRequest<ReadOnlyDockerContainerWidget> request, CancellationToken cancellationToken)
    {
        using var client = Endpoint.GetClient();

        await client.Containers.RemoveContainerAsync(request.Context.ContainerId
                                                   , new()
                                                     {
                                                         Force = true
                                                     }
                                                   , cancellationToken);
    }
}