using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.BackgroundService;

public interface IMediatRBackground
{
    ValueTask Send<T>(T request, CancellationToken cancellationToken = default)
        where T : IRequest<Unit>;

    ValueTask<IRequest<Unit>> DequeueAsync<T>(CancellationToken cancellationToken);
}
