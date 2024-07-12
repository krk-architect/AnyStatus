using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AnyStatus.API.Attributes;
using AnyStatus.API.Widgets;

namespace AnyStatus.Plugins.NuGet;

[Category("NuGet")]
[DisplayName("NuGet Package Version")]
[Description("NuGet package version number")]
public class NuGetPackageVersionWidget
    : TextWidget
    , IRequireEndpoint<NuGetEndpoint>
    , IPollable
    , ICommonWidget
{
    [Order(10)]
    [Required]
    [DisplayName("NuGet Package")]
    [Description("The NuGet package id. For example: AnyStatus.API")]
    public string PackageId { get; set; }

    [Order(30)]
    [DisplayName("Pre-Release")]
    [Description("Turn on to include pre-release packages.")]
    public bool PreRelease { get; set; }

    [Order(20)]
    [Required]
    [EndpointSource]
    [DisplayName("Endpoint")]
    public string EndpointId { get; set; }
}