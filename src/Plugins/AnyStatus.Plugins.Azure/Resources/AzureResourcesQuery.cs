using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AnyStatus.API.Endpoints;
using AnyStatus.API.Services;
using AnyStatus.API.Widgets;
using AnyStatus.Plugins.Azure.API;
using AnyStatus.Plugins.Azure.API.Endpoints;

namespace AnyStatus.Plugins.Azure.Resources;

public class AzureResourcesQuery : AsyncStatusCheck<AzureResourcesWidget>, IEndpointHandler<AzureOAuthEndpoint>
{
    private readonly IDispatcher _dispatcher;

    public AzureResourcesQuery(IDispatcher dispatcher) { _dispatcher = dispatcher; }

    public AzureOAuthEndpoint Endpoint { get; set; }

    protected override async Task Handle(StatusRequest<AzureResourcesWidget> request, CancellationToken cancellationToken)
    {
        var api = new AzureApi(Endpoint);

        var resources = await api.GetResourcesAsync(request.Context.SubscriptionId);

        if (resources is null)
        {
            request.Context.Status = Status.None;
        }
        else
        {
            _dispatcher.Invoke(() =>
                               {
                                   var rs = new AzureResourcesSynchronizer(request.Context);
                                   rs.Synchronize(resources.Value.ToList()
                                                , request.Context
                                                         .OfType<AzureResourceWidget>()
                                                         .ToList());
                               });
        }
    }
}