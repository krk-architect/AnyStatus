using System;
using AnyStatus.API.Services;

namespace AnyStatus.Core.Tests.Integration;

internal class Dispatcher : IDispatcher
{
    public void Invoke(Action callback) => callback();

    public void InvokeAsync(Action callback) => callback();
}