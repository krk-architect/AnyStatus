namespace AnyStatus.Plugins.UptimeRobot.Models;

internal class UptimeRobotMonitor
{
    public string Id           { get; set; }
    public string FriendlyName { get; set; }
    public byte   Status       { get; set; }

    public string GetStatus() => Status switch
                                 {
                                     0 => AnyStatus.API.Widgets.Status.Paused
                                   , 1 => AnyStatus.API.Widgets.Status.None
                                   , 2 => AnyStatus.API.Widgets.Status.OK
                                   , 8 => AnyStatus.API.Widgets.Status.Warning
                                   , 9 => AnyStatus.API.Widgets.Status.Failed
                                   , _ => AnyStatus.API.Widgets.Status.Unknown
                                 };
}