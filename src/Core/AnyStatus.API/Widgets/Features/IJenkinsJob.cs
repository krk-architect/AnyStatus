using System;

namespace AnyStatus.API.Widgets;

public interface IJenkinsJob
{
    string Job { get; set; }

    string BuildNumber { get; set; }

    DateTime FinishTime { get; set; }

    TimeSpan Duration { get; set; }
    string   URL      { get; set; }
}