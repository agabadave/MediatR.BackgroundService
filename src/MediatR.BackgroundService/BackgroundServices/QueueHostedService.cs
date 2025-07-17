using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MediatR.BackgroundService.BackgroundServices;

internal class QueueHostedService : Microsoft.Extensions.Hosting.BackgroundService
{
    private readonly IMediatorBackground _queue;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<QueueHostedService> _logger;

    public QueueHostedService(
        IMediatorBackground queue,
        IServiceProvider serviceProvider,
        ILogger<QueueHostedService> logger)
    {
        _queue = queue;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var request = await _queue.DequeueAsync<IRequest<Unit>>(stoppingToken);

            try
            {
                using var scope = _serviceProvider.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                await mediator.Send(request, stoppingToken);
                _logger.LogInformation("Successfully executed background MediatR request: {RequestName}",
                    request.GetType().Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing background MediatR request");
            }
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Queue Hosted service has been stopped!!!");

        await base.StopAsync(cancellationToken);
    }
}
