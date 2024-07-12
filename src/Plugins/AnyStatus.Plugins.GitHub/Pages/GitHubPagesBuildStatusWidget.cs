using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AnyStatus.API.Attributes;
using AnyStatus.API.Widgets;
using AnyStatus.Plugins.GitHub.API;
using AnyStatus.Plugins.GitHub.API.Sources;

namespace AnyStatus.Plugins.GitHub.Pages;

[Category("GitHub")]
[DisplayName("GitHub Pages")]
[Description("View the current status of a GitHub Pages build")]
public class GitHubPagesBuildStatusWidget
    : StatusWidget
    , IRequireEndpoint<GitHubEndpoint>
    , ICommonWidget
    , IPollable
    , IOpenInApp
{
    [Required]
    [AsyncItemsSource(typeof(GitHubRepositorySource), true)]
    public string Repository { get; set; }

    [Browsable(false)]
    public string URL { get; set; }

    [Required]
    [EndpointSource]
    [DisplayName("Endpoint")]
    [Refresh(nameof(Repository))]
    public string EndpointId { get; set; }
}