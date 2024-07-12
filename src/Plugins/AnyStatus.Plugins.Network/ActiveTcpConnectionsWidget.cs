using System.ComponentModel;
using AnyStatus.API.Widgets;

namespace AnyStatus.Plugins.SystemInformation.Network;

[Category("Network")]
[DisplayName("Active TCP Connections")]
[Description("View the active TCP/IP connections on the local computer")]
public class ActiveTcpConnectionsWidget : MetricWidget, IPollable, ICommonWidget { }