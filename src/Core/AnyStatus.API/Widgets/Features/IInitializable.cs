using System.Threading;
using System.Threading.Tasks;
using AnyStatus.API.Common;
using MediatR;

namespace AnyStatus.API.Widgets;

public interface IInitializable { }

public class InitializeRequest<T> : Request<T>
    where T : IInitializable
{
    public InitializeRequest(T context) : base(context) { }
}

public static class InitializeRequestFactory
{
    public static InitializeRequest<T> Create<T>(T context)
        where T : IInitializable => new (context);
}

internal interface IInitialize<T> : IRequestHandler<InitializeRequest<T>>
    where T : IInitializable { }

public abstract class AsyncIInitializeRequestHandler<TWidget> : AsyncRequestHandler<InitializeRequest<TWidget>>, IInitialize<TWidget>
    where TWidget : IInitializable
{
    protected abstract override Task Handle(InitializeRequest<TWidget> request, CancellationToken cancellationToken);
}