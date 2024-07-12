﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using AnyStatus.API.Attributes;
using AnyStatus.API.Common;

namespace AnyStatus.API.Endpoints;

[DebuggerDisplay("{Id}  |  {Name}  |  {Address}")]
public abstract class Endpoint : NotifyPropertyChanged, IEndpoint
{
    private string _address;
    private string _id;
    private string _name;

    [Browsable(false)]
    public string Id
    {
        get => _id;
        set => Set(ref _id, value);
    }

    [Required]
    [Order(1)]
    public string Name
    {
        get => _name;
        set => Set(ref _name, value);
    }

    [Required]
    [Order(20)]
    public string Address
    {
        get => _address;
        set => Set(ref _address, value);
    }
}