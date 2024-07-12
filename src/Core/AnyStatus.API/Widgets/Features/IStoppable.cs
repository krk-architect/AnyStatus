using System.Threading;
using System.Threading.Tasks;
using AnyStatus.API.Common;
using MediatR;

namespace AnyStatus.API.Widgets;

public interface IStoppable : IActionable
{
    bool CanStop { get; }
}

internal interface IStop<T> : IRequestHandler<StopRequest<T>>
    where T : IStoppable { }

public static class StopRequestFactory
{
    public static StopRequest<T> Create<T>(T context)
        where T : IStoppable => new (context);
}

public class StopRequest<T> : Request<T>
    where T : IStoppable
{
    public StopRequest(T context) : base(context) { }
}

public abstract class AsyncStopRequestHandler<TWidget> : AsyncRequestHandler<StopRequest<TWidget>>, IStop<TWidget>
    where TWidget : IStoppable
{
    protected abstract override Task Handle(StopRequest<TWidget> request, CancellationToken cancellationToken);
}