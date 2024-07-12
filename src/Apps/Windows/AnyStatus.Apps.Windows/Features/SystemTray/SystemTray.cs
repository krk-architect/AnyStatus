using System;
using System.Diagnostics;
using System.Windows.Forms;
using AnyStatus.API.Common;
using AnyStatus.API.Notifications;
using AnyStatus.Apps.Windows.Features.App;
using AnyStatus.Apps.Windows.Infrastructure.Mvvm.Windows;
using AnyStatus.Core.App;
using MediatR;
using Microsoft.Win32;

namespace AnyStatus.Apps.Windows.Features.SystemTray;

public sealed class SystemTray : NotifyPropertyChanged, ISystemTray
{
    private readonly IAppContext      _context;
    private readonly ContextMenuStrip _contextMenu;
    private readonly IMediator        _mediator;
    private readonly NotifyIcon       _notifier;
    private          bool             _disposed;
    private          string           _status;

    public SystemTray(IMediator mediator, IAppContext context)
    {
        _mediator = mediator;
        _context  = context;

        _contextMenu = new ContextMenuFactory(mediator, context).Create();

        _notifier = new()
                    {
                        Visible          = true
                      , Text             = "AnyStatus"
                      , ContextMenuStrip = _contextMenu
                      , Icon             = SystemTrayIcons.Get(API.Widgets.Status.OK)
                    };

        WireEvents();
    }

    public string Status
    {
        get => _status;
        set
        {
            Debug.WriteLine($"{nameof(SystemTray)}.{nameof(Status)} set   {nameof(value)}={value}");
            Set(ref _status, value);
            SetIcon(null, null);
        }
    }

    public void ShowNotification(Notification notification)
    {
        const string DefaultTitle = "AnyStatus";

        if (notification is null)
        {
            throw new ArgumentNullException(nameof(notification));
        }

        var icon = notification.Icon switch
                   {
                       NotificationIcon.Info    => ToolTipIcon.Info
                     , NotificationIcon.Error   => ToolTipIcon.Error
                     , NotificationIcon.Warning => ToolTipIcon.Warning
                     , _                        => ToolTipIcon.None
                   };

        _notifier.ShowBalloonTip(1000, string.IsNullOrEmpty(notification.Title) ? DefaultTitle : notification.Title, notification.Message, icon);
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        UnWireEvents();

        if (_notifier != null)
        {
            _notifier.Icon?.Dispose();
            _notifier.Dispose();
        }

        //_contextMenu.Dispose();

        _disposed = true;
    }

    private void WireEvents()
    {
        PropertyChanged                     += SetIcon;
        _notifier.MouseClick                += OnMouseClick;
        SystemEvents.DisplaySettingsChanged += SetIcon;
    }

    private void UnWireEvents()
    {
        PropertyChanged                     -= SetIcon;
        _notifier.MouseClick                -= OnMouseClick;
        SystemEvents.DisplaySettingsChanged -= SetIcon;
    }

    private void OnMouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            _mediator.Send(MaterialWindow.Show<AppViewModel>(width: 398, minWidth: 398, height: 418, minHeight: 418));
        }
    }

    private void SetIcon(object sender, EventArgs e)
    {
        var session      = _context.Session;
        var widget       = session.Widget;
        var widgetStatus = widget.Status;

        Debug.WriteLine($"{nameof(SystemTray)}.{nameof(SetIcon)}   {nameof(Status)}={Status}   {nameof(widgetStatus)}={widgetStatus}");

        _notifier.Icon = SystemTrayIcons.Get(Status);
    }
}