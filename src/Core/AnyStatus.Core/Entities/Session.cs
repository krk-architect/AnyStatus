﻿using System.Diagnostics;
using AnyStatus.API.Common;
using AnyStatus.API.Widgets;
using Newtonsoft.Json;

namespace AnyStatus.Core.Sessions;

[DebuggerDisplay("Widget={Widget}  ***  SelectedWidget={SelectedWidget}  ***  IsDirty={IsDirty}")]
public class Session : NotifyPropertyChanged
{
    private bool    _isDirty;
    private IWidget _selectedWidget;
    private IWidget _widget;

    public string FileName { get; set; }

    [JsonIgnore]
    public bool IsDirty
    {
        get => _isDirty;
        set => Set(ref _isDirty, value);
    }

    public bool IsNotDirty => !IsDirty;

    [JsonIgnore]
    public IWidget Widget
    {
        get => _widget;
        set => Set(ref _widget, value);
    }

    [JsonIgnore]
    public IWidget SelectedWidget
    {
        get => _selectedWidget;
        set => Set(ref _selectedWidget, value);
    }
}