using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AnyStatus.API.Widgets;

namespace AnyStatus.Plugins.HealthChecks;

[DisplayName("Ping")]
[Category("Network")]
[Description("Monitor the reachability of a host address by sending periodic health checks")]
public class PingHealthCheckWidget : StatusWidget, IPollable, ICommonWidget
{
    [Required]
    [Category("Ping")]
    [Description("The host name or IP address.")]
    public string Host { get; set; }
}