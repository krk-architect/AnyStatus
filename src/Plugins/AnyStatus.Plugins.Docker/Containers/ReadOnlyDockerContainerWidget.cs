using System.ComponentModel;
using AnyStatus.API.Widgets;

namespace AnyStatus.Plugins.Docker.Containers;

[Browsable(false)]
public class ReadOnlyDockerContainerWidget
    : Widget
    , IRequireEndpoint<DockerEndpoint>
    , IStartable
    , IStoppable
    , IRestartable
    , IRemovable
{
    public string ContainerId { get; set; }

    public string EndpointId { get; set; }

    public bool CanStart => Status != API.Widgets.Status.OK;

    public bool CanStop => Status == API.Widgets.Status.OK;
}