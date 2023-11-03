using System.Diagnostics;
using AnyStatus.API.Common;

namespace AnyStatus.API.Widgets
{
    [DebuggerDisplay("{IsEnabled}")]
    public class WidgetNotificationSettings : NotifyPropertyChanged
    {
        private bool _isEnabled;

        public bool IsEnabled
        {
            get => _isEnabled;
            set => Set(ref _isEnabled, value);
        }
    }
}
