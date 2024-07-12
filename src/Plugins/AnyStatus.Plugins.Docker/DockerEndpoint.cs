using System.ComponentModel;
using AnyStatus.API.Endpoints;

namespace AnyStatus.Plugins.Docker;

[DisplayName("Docker")]
public class DockerEndpoint : Endpoint
{
    public DockerEndpoint()
    {
        Name    = "Docker Desktop";
        Address = "npipe://./pipe/docker_engine";
    }
}