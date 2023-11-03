using System;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;

namespace AnyStatus.Plugins.Jenkins.API.Models
{
    [DebuggerDisplay("{ToString()}")]
    public class JenkinsJob
    {
        public string Name { get; set; }

        private string _url = "";
        public string URL
        {
            get => _url;
            set
            {
                _url = value;

                if (string.IsNullOrWhiteSpace(_url) || !string.IsNullOrEmpty(Name))
                {
                    return;
                }

                var segments = _url.Split('/', StringSplitOptions.RemoveEmptyEntries).Reverse().ToArray();

                BuildNumber = segments[0];
                Name        = segments[1];
            }
        }

        public string             BuildNumber       { get; set; }
        public int                Duration          { get; set; }
        public int                EstimatedDuration { get; set; }
        public long               Timestamp         { get; set; }
        public string             Result            { get; set; }
        public bool               InProgress        { get; set; } // different between v1 and v2
        public JenkinsJobProgress Executor          { get; set; }

        [JsonProperty("building")] public bool IsRunning { get; set; }

        public bool IsCurrentlyRunning() => IsRunning || InProgress;

        public TimeSpan GetCurrentlyRunningDuration()
        {
            if (!IsCurrentlyRunning())
            {
                return TimeSpan.Zero;
            }

            var start    = DateTimeOffset.FromUnixTimeMilliseconds(Timestamp).DateTime;
            var startUtc = DateTime.SpecifyKind(start, DateTimeKind.Utc);
            var now      = DateTime.UtcNow;
            var duration = now - start;
            return duration;

        }

        public string Status => IsCurrentlyRunning()
            ? AnyStatus.API.Widgets.Status.Running
            : Result switch
              {
                  "SUCCESS"  => AnyStatus.API.Widgets.Status.OK
                , "ABORTED"  => AnyStatus.API.Widgets.Status.Canceled
                , "FAILURE"  => AnyStatus.API.Widgets.Status.Failed
                , "UNSTABLE" => AnyStatus.API.Widgets.Status.PartiallySucceeded
                , "QUEUED"   => AnyStatus.API.Widgets.Status.Queued
                , _          => AnyStatus.API.Widgets.Status.Unknown
              };

        public override string ToString() => $"{Status,8} | {Name} | {URL}";
    }
}