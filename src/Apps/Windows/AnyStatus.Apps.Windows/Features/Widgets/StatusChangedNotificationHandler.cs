using System.Xml.Linq;
using AnyStatus.API.Events;
using AnyStatus.API.Notifications;
using AnyStatus.API.Widgets;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AnyStatus.Apps.Windows.Features.Widgets
{
    public class StatusChangedNotificationHandler<TWidget> : NotificationHandler<StatusChangedNotification<TWidget>>
        where TWidget : class, IWidget
    {
        private readonly ILogger _logger;
        private readonly INotificationService _notificationService;

        public StatusChangedNotificationHandler(INotificationService notificationService, ILogger logger)
        {
            _logger = logger;
            _notificationService = notificationService;
        }

        protected override void Handle(StatusChangedNotification<TWidget> notification)
        {
            var previousStatus = notification.Widget.PreviousStatus?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(previousStatus))
            {
                return; // no sense showing that a status changed from something to something else when it never had a status before
            }

            var currentStatus = notification.Widget.Status;
            var message       = "";

            var name = notification.Widget.Name;
            if (name.ToLower() == "all")
            {
                message = $"System Tray STATUS CHANGED  [{currentStatus}  -->  {previousStatus}]";
            }
            else
            {
                message = $"Widget STATUS CHANGED  [{currentStatus}  -->  {previousStatus}]      \"{name}\"";
            }

            if (_logger.IsEnabled(LogLevel.Trace))
            {
                _logger.LogTrace(message);
            }

            if (previousStatus != Status.None)
            {
                _notificationService.Send(new Notification(message, name));
            }
        }
    }
}
