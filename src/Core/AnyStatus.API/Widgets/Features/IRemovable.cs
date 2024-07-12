using System.Threading;
using System.Threading.Tasks;
using AnyStatus.API.Common;
using MediatR;

namespace AnyStatus.API.Widgets;

public interface IRemovable : IActionable { }

internal interface IRemove<T> : IRequestHandler<RemoveRequest<T>>
    where T : IRemovable { }

public static class RemoveRequestFactory
{
    public static RemoveRequest<T> Create<T>(T context)
        where T : IRemovable => new (context);
}

public class RemoveRequest<T> : Request<T>
    where T : IRemovable
{
    public RemoveRequest(T context) : base(context) { }
}

public abstract class AsyncRemoveRequestHandler<TWidget> : AsyncRequestHandler<RemoveRequest<TWidget>>, IRemove<TWidget>
    where TWidget : IRemovable
{
    protected abstract override Task Handle(RemoveRequest<TWidget> request, CancellationToken cancellationToken);
}