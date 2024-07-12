using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AnyStatus.API.Endpoints;
using AnyStatus.API.Widgets;
using AnyStatus.Core.Extensions;
using AnyStatus.Plugins.Azure.API;
using AnyStatus.Plugins.Azure.API.Contracts;
using AnyStatus.Plugins.Azure.API.Endpoints;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AnyStatus.Plugins.Azure.DevOps.Builds;

public class AzureDevOpsPipelineStatusCheck : AsyncStatusCheck<AzureDevOpsPipelineWidget>, IEndpointHandler<IAzureDevOpsEndpoint>
{
    public AzureDevOpsPipelineStatusCheck(IMapper mapper, ILogger logger)
    {
        Logger = logger;
        Mapper = mapper;
    }

    public  ILogger              Logger   { get; }
    private IMapper              Mapper   { get; }
    public  IAzureDevOpsEndpoint Endpoint { get; set; }

    protected override async Task Handle(StatusRequest<AzureDevOpsPipelineWidget> request, CancellationToken cancellationToken)
    {
        Build build = null;

        try
        {
            var api = new AzureDevOpsApi(Endpoint);

            var builds = await api.GetBuildsAsync(request.Context.Account
                                                , request.Context.Project
                                                , request.Context.DefinitionId
                                                , 1)
                                  .ConfigureAwait(false);

            build = builds?.Value?.FirstOrDefault();
        }
        catch (Exception e)
        {
            if (e.IsTimeout())
            {
                Logger.LogWarning($"*** TIMEOUT ***  {request.Context.Name}");
                return;
            }

            Console.WriteLine(e);
            throw;
        }

        if (build is null)
        {
            Logger.LogWarning($"*** STATUS UNKNOWN ***  {request.Context.Name}");
            request.Context.Reset();
            request.Context.Status = Status.Unknown;
            return;
        }

        var name           = request.Context.Name;
        var previousStatus = request.Context.Status?.Trim() ?? "";
        var currentStatus  = build.GetStatus?.Trim()        ?? "";

        if (!string.IsNullOrWhiteSpace(previousStatus)
         && !string.IsNullOrWhiteSpace(currentStatus)
         && previousStatus != currentStatus)
        {
            var message = $"STATUS CHANGED  [{currentStatus}  -->  {previousStatus}]      \"{name}\"";

            if (currentStatus == Status.Error)
            {
                Logger.LogCritical(message);
            }
            else
            {
                Logger.LogInformation(message);
            }
        }
        else if (currentStatus == Status.Running)
        {
            var duration  = build.Duration;
            var isRunning = duration < TimeSpan.FromMinutes(10); // TODO: how to figure out if the build is technically running, but waiting for approval in the pipeline?

            if (isRunning)
            {
                var message = $"[{currentStatus} ({duration})]      \"{name}\"";
                Logger.LogTrace(message);
            }
        }
        else if (currentStatus != Status.OK
              && currentStatus != Status.Canceled
              && currentStatus != Status.Disabled
              && currentStatus != Status.Canceled)
        {
            var message = $"[{currentStatus}]      \"{name}\"";

            Logger.LogTrace(message);
        }

        Mapper.Map(build, request.Context);
    }
}