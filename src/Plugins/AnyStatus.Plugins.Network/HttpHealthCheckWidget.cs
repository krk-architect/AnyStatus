using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using AnyStatus.API.Widgets;

namespace AnyStatus.Plugins.HealthChecks;

[DisplayName("HTTP")]
[Category("Network")]
[Description("Monitor the health of HTTP servers by sending periodic health checks")]
public class HttpHealthCheckWidget
    : StatusWidget
    , IPollable
    , IOpenInApp
    , ICommonWidget
{
    [DisplayName("HTTP Status Code")]
    public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.OK;

    [DisplayName("Ignore SSL errors")]
    public bool IgnoreSslErrors { get; set; }

    [DisplayName("Use Default Credentials")]
    public bool UseDefaultCredentials { get; set; }

    [Required]
    public string URL { get; set; }
}