using System.Collections.ObjectModel;
using AnyStatus.API.Endpoints;
using AnyStatus.Core.Sessions;
using AnyStatus.Core.Settings;

namespace AnyStatus.Core.App;

public interface IAppContext
{
    Session Session { get; set; }

    UserSettings UserSettings { get; set; }

    ObservableCollection<IEndpoint> Endpoints { get; set; }
}