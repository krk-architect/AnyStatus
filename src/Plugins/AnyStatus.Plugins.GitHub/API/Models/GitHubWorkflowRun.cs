namespace AnyStatus.Plugins.GitHub.API.Models;

internal class GitHubWorkflowRun
{
    public string Status     { get; set; }
    public string Conclusion { get; set; }

    public string GetStatus() => Status switch
                                 {
                                     "queued"                                   => AnyStatus.API.Widgets.Status.Queued
                                   , "in_progress"                              => AnyStatus.API.Widgets.Status.Running
                                   , "completed" when Conclusion == "success"   => AnyStatus.API.Widgets.Status.OK
                                   , "completed" when Conclusion == "cancelled" => AnyStatus.API.Widgets.Status.Canceled
                                   , "completed" when Conclusion == "failure"   => AnyStatus.API.Widgets.Status.Failed
                                   , _                                          => AnyStatus.API.Widgets.Status.Unknown
                                 };
}