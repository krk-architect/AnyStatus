using System;
using AnyStatus.API.Dialogs;
using AnyStatus.API.Notifications;
using AnyStatus.API.Services;
using AnyStatus.Core.Services;
using NSubstitute;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace AnyStatus.Core.Tests.Integration;

public sealed class ContainerFixture : IDisposable
{
    public ContainerFixture()
    {
        Container = new ();

        Container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

        Container.RegisterPackages(Scanner.GetAssemblies());

        Container.RegisterInstance(Substitute.For<IDialogService>());

        Container.RegisterInstance(Substitute.For<INotificationService>());

        Container.RegisterInstance<IDispatcher>(new Dispatcher());

        Container.Options.ResolveUnregisteredConcreteTypes = true;

        Container.Verify();
    }

    public Container Container { get; }

    public void Dispose() => Container.Dispose();
}