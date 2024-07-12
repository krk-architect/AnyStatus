using System.ComponentModel;
using AnyStatus.API.Widgets;
using AnyStatus.Plugins.Azure.API.Endpoints;

namespace AnyStatus.Plugins.Azure.Resources;

[Browsable(false)]
public class AzureResourceWidget : StatusWidget, IPollable, IRequireEndpoint<IAzureEndpoint>
{
    public string ResourceId { get; set; }

    public string Kind { get; set; }

    public string Type { get; set; }

    public string Location   { get; set; }
    public string EndpointId { get; set; }
}