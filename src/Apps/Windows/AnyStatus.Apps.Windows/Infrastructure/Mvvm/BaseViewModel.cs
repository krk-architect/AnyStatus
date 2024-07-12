using System.Collections.Generic;
using System.Windows.Input;
using AnyStatus.API.Common;

namespace AnyStatus.Apps.Windows.Infrastructure.Mvvm;

public abstract class BaseViewModel : NotifyPropertyChanged
{
    public Dictionary<string, ICommand> Commands { get; } = [];
}