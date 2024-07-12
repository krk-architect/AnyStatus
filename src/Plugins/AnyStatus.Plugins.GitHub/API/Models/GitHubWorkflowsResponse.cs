using System.Collections.Generic;

namespace AnyStatus.Plugins.GitHub.API.Models;

internal class GitHubWorkflowsResponse
{
    public IEnumerable<GitHubWorkflow> Workflows { get; set; }
}