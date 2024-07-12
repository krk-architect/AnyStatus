using System;
using AnyStatus.API.Notifications;

namespace AnyStatus.Apps.Windows.Features.SystemTray;

public interface ISystemTray : IDisposable
{
    string Status { get; set; }

    void ShowNotification(Notification notification);
}