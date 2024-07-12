using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AnyStatus.API.Endpoints;
using AnyStatus.API.Services;
using AnyStatus.API.Widgets;
using Docker.DotNet.Models;

namespace AnyStatus.Plugins.Docker.Images;

public class DockerImagesHealthCheck : AsyncStatusCheck<DockerImagesWidget>, IEndpointHandler<DockerEndpoint>
{
    private readonly IDispatcher _dispatcher;

    public DockerImagesHealthCheck(IDispatcher dispatcher) { _dispatcher = dispatcher; }

    public DockerEndpoint Endpoint { get; set; }

    protected override async Task Handle(StatusRequest<DockerImagesWidget> request, CancellationToken cancellationToken)
    {
        ICollection<ImagesListResponse> images;

        using (var client = Endpoint.GetClient())
        {
            images = await client.Images.ListImagesAsync(new()
                                                         {
                                                             All = true
                                                         })
                                 .ConfigureAwait(false);
        }

        if (images is null)
        {
            request.Context.Status = Status.Unknown;
        }
        else if (images.Count > 0)
        {
            _dispatcher.Invoke(() =>
                               {
                                   var ds = new DockerImagesSynchronizer(request.Context);
                                   ds.Synchronize(images
                                                , request.Context
                                                         .OfType<ReadOnlyDockerImage>()
                                                         .ToList());
                               });
        }
        else
        {
            request.Context.Status = Status.OK;
        }
    }
}