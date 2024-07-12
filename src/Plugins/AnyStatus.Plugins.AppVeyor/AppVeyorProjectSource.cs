using System.Collections.Generic;
using System.Threading.Tasks;
using AnyStatus.API.Attributes;
using AnyStatus.API.Endpoints;
using AnyStatus.Plugins.AppVeyor.API;

namespace AnyStatus.Plugins.AppVeyor;

public class AppVeyorProjectSource : IAsyncItemsSource
{
    private readonly IEndpointProvider _endpointsProvider;

    public AppVeyorProjectSource(IEndpointProvider endpointsProvider)
    {
        _endpointsProvider = endpointsProvider;
    }

    public async Task<IEnumerable<NameValueItem>> GetItemsAsync(object source)
    {
        var results = new List<NameValueItem>();

        if (source is AppVeyorBuildWidget widget && !string.IsNullOrEmpty(widget.EndpointId) && _endpointsProvider.GetEndpoint(widget.EndpointId) is AppVeyorEndpoint endpoint)
        {
            var response = await new AppVeyorAPI(endpoint).GetProjectsAsync();

            foreach (var project in response)
            {
                results.Add(new (project.Name, project.Slug));
            }
        }

        return results;
    }
}