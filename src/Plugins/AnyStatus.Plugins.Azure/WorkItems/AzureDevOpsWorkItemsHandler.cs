using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AnyStatus.API.Endpoints;
using AnyStatus.API.Services;
using AnyStatus.API.Widgets;
using AnyStatus.Plugins.Azure.API;
using AnyStatus.Plugins.Azure.API.Endpoints;
using AutoMapper;

namespace AnyStatus.Plugins.Azure.WorkItems;

public class AzureDevOpsWorkItemsHandler : AsyncStatusCheck<AzureDevOpsWorkItemsWidget>, IEndpointHandler<IAzureDevOpsEndpoint>
{
    private const string query = """
                                 SELECT [System.Id]
                                 FROM WorkItems
                                 WHERE [System.AssignedTo] = @Me
                                   AND [System.IterationPath] = {0}
                                 """;

    private readonly IDispatcher _dispatcher;

    private readonly IMapper _mapper;

    public AzureDevOpsWorkItemsHandler(IMapper mapper, IDispatcher dispatcher)
    {
        _mapper     = mapper;
        _dispatcher = dispatcher;
    }

    public IAzureDevOpsEndpoint Endpoint { get; set; }

    protected override async Task Handle(StatusRequest<AzureDevOpsWorkItemsWidget> request, CancellationToken cancellationToken)
    {
        var api = new AzureDevOpsApi(Endpoint);

        var wiql = string.Format(query, request.Context.Iteration);

        var response = await api.QueryWorkItemsAsync(request.Context.Account
                                                   , request.Context.Project
                                                   , wiql
                                                   , cancellationToken);

        if (response.WorkItems.Any())
        {
            var ids = response.WorkItems.Select(w => w.Id).ToList();

            var workItems = await api.GetWorkItemsAsync(request.Context.Account
                                                      , request.Context.Project
                                                      , ids
                                                      , cancellationToken);

            request.Context.Text = workItems.Count.ToString();

            _dispatcher.Invoke(() =>
                               {
                                   var ws = new AzureDevOpsWorkItemsSynchronizer(_mapper, request.Context);
                                   ws.Synchronize(workItems.Value.ToList()
                                                , request.Context
                                                         .OfType<AzureDevOpsWorkItemWidget>()
                                                         .ToList());
                               });
        }
        else
        {
            request.Context.Text = default;

            _dispatcher.Invoke(request.Context.Clear);
        }

        request.Context.Status = Status.OK;
    }
}