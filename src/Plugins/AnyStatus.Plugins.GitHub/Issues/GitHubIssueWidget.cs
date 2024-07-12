using System.ComponentModel;
using AnyStatus.API.Widgets;

namespace AnyStatus.Plugins.GitHub.Issues;

[Browsable(false)]
public class GitHubIssueWidget : Widget, IOpenInApp
{
    public string Number { get; set; }

    public string URL { get; set; }
}