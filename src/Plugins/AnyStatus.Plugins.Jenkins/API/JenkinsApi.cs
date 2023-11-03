using AnyStatus.Plugins.Jenkins.API.Models;
using RestSharp;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnyStatus.Plugins.Jenkins.API
{
    internal class JenkinsApi
    {
        private readonly IRestClient _client;
        private readonly JenkinsEndpoint _endpoint;

        public JenkinsApi(JenkinsEndpoint endpoint)
        {
            _endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));

            _client = new RestClient(endpoint.Address)
            {
                Authenticator = endpoint.GetAuthenticator()
            };

            if (endpoint.IgnoreSslErrors)
            {
                _client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            }
        }

        private async Task<T> ExecuteAsync<T>(IRestRequest request, CancellationToken cancellationToken) where T : new()
        {
            var requestUrl = request.GetUrl();
            var response   = await _client.ExecuteAsync<T>(request, cancellationToken).ConfigureAwait(false);

            if (response.IsSuccessful && response.ErrorException is null)
            {
                var responseData = response.Data;
                return responseData;
            }

            throw new Exception("An error response received from Jenkins server.", response.ErrorException);
        }

        public Task<JenkinsJobsResponse> GetJobsAsync(CancellationToken cancellationToken)
        {
            var request = new RestRequest("/api/json");

            request.AddParameter("tree", "jobs[name,url]");

            return ExecuteAsync<JenkinsJobsResponse>(request, cancellationToken);
        }

        public Task<JenkinsJob> GetJobAsync(string job, CancellationToken cancellationToken)
        {
            var request = new RestRequest(_endpoint.Address + job + "lastBuild/api/json");//todo: remove redundant _endpoint.Address

            request.AddParameter("tree", "result,building,executor[progress],url,duration,estimatedDuration,timestamp,inProgress");

            return ExecuteAsync<JenkinsJob>(request, cancellationToken);
        }
    }

    public static class ApiExtensions
    {
        public static string GetUrl(this IRestRequest request)
        {
            var sb = new StringBuilder();
            sb.Append(request.Resource);
            for (var i = 0; i < request.Parameters.Count; i++)
            {
                var p         = request.Parameters[i];
                var separator = i == 0 ? '?' : '&';

                if (p.Type == ParameterType.GetOrPost   ||
                    p.Type == ParameterType.QueryString ||
                    p.Type == ParameterType.UrlSegment)
                {
                    sb.Append($"{separator}{p.Name}={p.Value}");
                }
            }
            return sb.ToString();
        }
    }

}
