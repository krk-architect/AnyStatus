using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AnyStatus.API.Attributes;
using AnyStatus.API.Widgets;
using Newtonsoft.Json;

namespace AnyStatus.Plugins.Jenkins.Jobs;

[Category("Jenkins")]
[DisplayName("Jenkins Job Status")]
[Description("View the status of jobs on Jenkins server")]
public class JenkinsJobWidget
    : StatusWidget
    , IRequireEndpoint<JenkinsEndpoint>
    , ICommonWidget
    , IJenkinsJob
    , IOpenInApp
    , IPollable
    , IProgress
{
    private string   _buildNumber;
    private TimeSpan _duration;
    private DateTime _finishTime;

    [Required]
    [DisplayName("Job")]
    [AsyncItemsSource(typeof(JenkinsJobsSource), true)]
    public string Job { get; set; }

    [JsonIgnore]
    [Browsable(false)]
    public string BuildNumber
    {
        get => _buildNumber;
        set => Set(ref _buildNumber, value);
    }

    [JsonIgnore]
    [Browsable(false)]
    public DateTime FinishTime
    {
        get => _finishTime;
        set => Set(ref _finishTime, value);
    }

    [JsonIgnore]
    [Browsable(false)]
    public TimeSpan Duration
    {
        get => _duration;
        set => Set(ref _duration, value);
    }

    [JsonIgnore]
    [Browsable(false)]
    public string URL { get; set; }

    [Required]
    [EndpointSource]
    [Refresh(nameof(Job))]
    [DisplayName("Endpoint")]
    public string EndpointId { get; set; }
}