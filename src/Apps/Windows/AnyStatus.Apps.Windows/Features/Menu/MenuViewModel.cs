using System.Threading.Tasks;
using AnyStatus.Apps.Windows.Features.App;
using AnyStatus.Apps.Windows.Features.Endpoints;
using AnyStatus.Apps.Windows.Features.Help;
using AnyStatus.Apps.Windows.Features.Settings;
using AnyStatus.Apps.Windows.Infrastructure.Mvvm;
using AnyStatus.Apps.Windows.Infrastructure.Mvvm.Pages;
using AnyStatus.Core.Features;
using MediatR;

// ReSharper disable AsyncVoidFunctionExpression
// ReSharper disable AsyncVoidLambda

#pragma warning disable AsyncFixer03 // Avoid fire & forget async void methods - not going to mess with it at this point.

namespace AnyStatus.Apps.Windows.Features.Menu;

public class MenuViewModel : BaseViewModel
{
    private readonly IMediator _mediator;
    private          bool      _isVisible;

    public MenuViewModel(IMediator mediator)
    {
        _mediator = mediator;

        Commands.Add("New",       new Command(async _ => await ExecuteNewSessionCommandAsync().ConfigureAwait(false)));
        Commands.Add("Open",      new Command(async _ => await ExecuteOpenSessionCommandAsync().ConfigureAwait(false)));
        Commands.Add("Save",      new Command(async _ => await ExecuteSaveCommandAsync().ConfigureAwait(false)));
        Commands.Add("SaveAs",    new Command(async _ => await ExecuteSaveAsCommandAsync().ConfigureAwait(false)));
        Commands.Add("Settings",  new Command(async _ => await ExecuteSettingsCommandAsync().ConfigureAwait(false)));
        Commands.Add("Endpoints", new Command(async _ => await ExecuteEndpointsCommandAsync().ConfigureAwait(false)));
        Commands.Add("Help",      new Command(async _ => await ExecuteHelpCommandAsync().ConfigureAwait(false)));
        Commands.Add("Exit",      new Command(async _ => await ExecuteExitCommandAsync().ConfigureAwait(false)));
    }

    public bool IsVisible
    {
        get => _isVisible;
        set => Set(ref _isVisible, value);
    }

    private async Task ExecuteNewSessionCommandAsync()
    {
        var result = await _mediator.Send(new NewSession.Request()).ConfigureAwait(false);
        IsVisible = !result;
    }

    private async Task ExecuteOpenSessionCommandAsync()
    {
        var result = await _mediator.Send(new OpenSession.Request()).ConfigureAwait(false);
        IsVisible = !result;
    }

    private async Task ExecuteSaveCommandAsync()
    {
        var result = await _mediator.Send(new Save.Request()).ConfigureAwait(false);
        IsVisible = !result;
    }

    private async Task ExecuteSaveAsCommandAsync()
    {
        var result = await _mediator.Send(new Save.Request(true)).ConfigureAwait(false);
        IsVisible = !result;
    }

    private async Task ExecuteSettingsCommandAsync()
    {
        await _mediator.Send(Page.Show<SettingsViewModel>("Settings")).ConfigureAwait(false);
        IsVisible = false;
    }

    private async Task ExecuteEndpointsCommandAsync()
    {
        await _mediator.Send(Page.Show<EndpointsViewModel>("Endpoints")).ConfigureAwait(false);
        IsVisible = false;
    }

    private async Task ExecuteHelpCommandAsync()
    {
        await _mediator.Send(Page.Show<HelpViewModel>("Help")).ConfigureAwait(false);
        IsVisible = false;
    }

    private Task<Unit> ExecuteExitCommandAsync() => _mediator.Send(new Shutdown.Request());
}