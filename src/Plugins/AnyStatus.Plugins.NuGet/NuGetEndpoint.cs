using System.ComponentModel;
using AnyStatus.API.Endpoints;

namespace AnyStatus.Plugins.NuGet;

[DisplayName("NuGet")]
public class NuGetEndpoint : Endpoint
{
    public NuGetEndpoint()
    {
        Name    = "NuGet";
        Address = "https://api.nuget.org/v3/index.json";
    }
}