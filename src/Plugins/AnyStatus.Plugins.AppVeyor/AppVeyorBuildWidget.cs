using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AnyStatus.API.Attributes;
using AnyStatus.API.Widgets;

namespace AnyStatus.Plugins.AppVeyor;

[Category("AppVeyor")]
[DisplayName("AppVeyor Build Status")]
[Description("View the status of build pipelines on AppVeyor")]
public class AppVeyorBuildWidget : StatusWidget, ICommonWidget, IPollable, IRequireEndpoint<AppVeyorEndpoint>, IOpenInApp
{
    [Required]
    [DisplayName("Project")]
    [AsyncItemsSource(typeof(AppVeyorProjectSource), true)]
    public string ProjectSlug { get; set; }

    [Description("Optional branch name")]
    public string Branch { get; set; }

    [Browsable(false)]
    public string URL { get; internal set; }

    [Required]
    [EndpointSource]
    [Refresh(nameof(ProjectSlug))]
    [DisplayName("Endpoint")]
    public string EndpointId { get; set; }
}