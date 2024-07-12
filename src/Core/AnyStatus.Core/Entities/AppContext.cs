using System.Collections.ObjectModel;
using System.Diagnostics;
using AnyStatus.API.Common;
using AnyStatus.API.Endpoints;
using AnyStatus.Core.Sessions;
using AnyStatus.Core.Settings;

namespace AnyStatus.Core.App;

[DebuggerDisplay("{Session}  ###  {(Endpoints?.Count ?? 0)} Endpoints")]
public class AppContext : NotifyPropertyChanged, IAppContext
{
    private ObservableCollection<IEndpoint> _endpoints;
    private Session                         _session;
    private UserSettings                    _userSettings;

    public Session Session
    {
        get => _session;
        set => Set(ref _session, value);
    }

    public UserSettings UserSettings
    {
        get => _userSettings;
        set => Set(ref _userSettings, value);
    }

    public ObservableCollection<IEndpoint> Endpoints
    {
        get => _endpoints;
        set => Set(ref _endpoints, value);
    }
}