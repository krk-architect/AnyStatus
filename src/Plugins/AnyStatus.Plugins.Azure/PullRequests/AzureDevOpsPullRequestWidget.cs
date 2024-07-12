using System.ComponentModel;
using AnyStatus.API.Widgets;

namespace AnyStatus.Plugins.Azure.DevOps.PullRequests;

[Browsable(false)]
public class AzureDevOpsPullRequestWidget : StatusWidget
{
    public object PullRequestId { get; internal set; }
}