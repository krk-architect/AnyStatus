using System;
using AnyStatus.API.Endpoints;
using AnyStatus.API.Widgets;
using AnyStatus.Plugins.Jenkins.API;
using System.Threading;
using System.Threading.Tasks;
using AnyStatus.Plugins.Jenkins.API.Models;

namespace AnyStatus.Plugins.Jenkins.Jobs
{
    public class JenkinsJobHealthCheck : AsyncStatusCheck<JenkinsJobWidget>, IEndpointHandler<JenkinsEndpoint>
    {
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
                if (e.Message.Contains("Timeout", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine("*** TIMEOUT ***");
                    return;
                }

                Console.WriteLine(e);
                throw;
            }

            if (job is null)
            {
                request.Context.Status = Status.Unknown;
                Console.WriteLine("*** STATUS UNKNOWN ***");
                return;
            }

            request.Context.Status      = job.Status;
            request.Context.URL         = job.URL;
            request.Context.BuildNumber = job.BuildNumber;
            request.Context.FinishTime  = DateTimeOffset.FromUnixTimeMilliseconds(job.Timestamp).UtcDateTime;
            request.Context.Duration    = TimeSpan.FromMilliseconds(job.Duration);
        }
    }
}
