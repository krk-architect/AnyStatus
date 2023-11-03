using System;
using AnyStatus.API.Endpoints;
using AnyStatus.API.Widgets;
using AnyStatus.Plugins.Jenkins.API;
using System.Threading;
using System.Threading.Tasks;
using AnyStatus.Core.Extensions;
using AnyStatus.Plugins.Jenkins.API.Models;
using Microsoft.Extensions.Logging;

namespace AnyStatus.Plugins.Jenkins.Jobs
{
    public class JenkinsJobHealthCheck : AsyncStatusCheck<JenkinsJobWidget>, IEndpointHandler<JenkinsEndpoint>
    {
        public JenkinsJobHealthCheck(ILogger logger) => Logger = logger;

        public ILogger         Logger { get; }
        public JenkinsEndpoint Endpoint { get; set; }

        protected override async Task Handle(StatusRequest<JenkinsJobWidget> request, CancellationToken cancellationToken)
        {
            var        api = new JenkinsApi(Endpoint);
            JenkinsJob job;

            try
            {
                job = await api.GetJobAsync(request.Context.Job, cancellationToken).ConfigureAwait(false);

            }
            catch (Exception e)
            {
                if (e.IsTimeout())
                {
                    Logger.LogWarning($"*** STATUS UNKNOWN ***  {request.Context.Job}");
                    return;
                }

                Console.WriteLine(e);
                throw;
            }

            if (job is null)
            {
                Logger.LogWarning($"*** STATUS UNKNOWN ***  {request.Context.Job}");
                request.Context.Status = Status.Unknown;
                return;
            }

            var name = request.Context.Name;
            var previousStatus = request.Context.Status?.Trim() ?? "";
            var currentStatus  = job.Status?.Trim() ?? "";

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
            else if (job.IsCurrentlyRunning())
            {
                var duration = job.GetCurrentlyRunningDuration();
                var message  = $"[{currentStatus} ({duration})]      \"{name}\"";
                Logger.LogTrace(message);
            }
            else if (currentStatus != Status.OK       &&
                     currentStatus != Status.Canceled &&
                     currentStatus != Status.Disabled &&
                     currentStatus != Status.Canceled)
            {
                var message = $"[{currentStatus}]      \"{name}\"";
                Logger.LogTrace(message);
            }

            request.Context.Status      = currentStatus;
            request.Context.URL         = job.URL;
            request.Context.BuildNumber = job.BuildNumber;
            request.Context.FinishTime  = DateTimeOffset.FromUnixTimeMilliseconds(job.Timestamp).UtcDateTime;
            request.Context.Duration    = TimeSpan.FromMilliseconds(job.Duration);
        }
    }
}
