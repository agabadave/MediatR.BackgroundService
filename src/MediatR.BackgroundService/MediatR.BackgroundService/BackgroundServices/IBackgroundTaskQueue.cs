using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.BackgroundService.BackgroundServices
{
    internal interface IBackgroundTaskQueue
    {
        ValueTask Enqueue(Func<CancellationToken, ValueTask> workItem);

        ValueTask<Func<CancellationToken, ValueTask>> DequeueAsync(
            CancellationToken cancellationToken);
    }
}
