using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace MediatR.BackgroundService.BackgroundServices
{
    internal class BackgroundTaskQueue : IMediatRBackground
    {
        private readonly Channel<IRequest<Unit>> _queue = Channel.CreateUnbounded<IRequest<Unit>>();
        private readonly ILogger<BackgroundTaskQueue> _logger;

        public BackgroundTaskQueue(ILogger<BackgroundTaskQueue> logger)
        {
            var queueCapacity = 10; //refer to docs on setting the capacity
            var options = new BoundedChannelOptions(queueCapacity)
            {
                FullMode = BoundedChannelFullMode.Wait
            };
            _queue = Channel.CreateBounded<IRequest<Unit>>(options);

            _logger = logger;
        }

        public ValueTask Send<T>(T request, CancellationToken cancellationToken) where T : IRequest<Unit>
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            _logger.LogInformation("Preparing to write item {requestName} to background queue.", nameof(request));

            return _queue.Writer.WriteAsync(request, cancellationToken);

        }

        public ValueTask<IRequest<Unit>> DequeueAsync<T>(CancellationToken cancellationToken)
        {
            return _queue.Reader.ReadAsync(cancellationToken);
        }

    }
}