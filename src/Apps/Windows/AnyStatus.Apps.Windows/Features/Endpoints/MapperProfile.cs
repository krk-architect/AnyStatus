using AnyStatus.API.Endpoints;
using AnyStatus.Core.Services;
using AutoMapper;

namespace AnyStatus.Apps.Windows.Features.Endpoints;

internal class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<IEndpoint, EndpointViewModel>();
        CreateMap<SaveEndpoint.Request, IEndpoint>();
        CreateMap<EndpointViewModel, SaveEndpoint.Request>();

        foreach (var endpoint in Scanner.GetTypesOf<IEndpoint>())
        {
            CreateMap(endpoint, endpoint);
        }
    }
}