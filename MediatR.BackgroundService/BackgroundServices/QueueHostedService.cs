using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MediatR.BackgroundService.BackgroundServices
{
    internal class QueueHostedService : Microsoft.Extensions.Hosting.BackgroundService
    {
        private readonly ILogger<QueueHostedService> _logger;

        public QueueHostedService(IBackgroundTaskQueue taskQueue, ILogger<QueueHostedService> logger)
        {
            TaskQueue = taskQueue;
            _logger = logger;
        }

        public IBackgroundTaskQueue TaskQueue { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Queued Hosted Service is running...");

            await BackgroundProcessing(stoppingToken);
        }

        private async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await TaskQueue.DequeueAsync(stoppingToken);

                try
                {
                    _logger.LogInformation("Starting execution of {workItem}...", nameof(workItem));

                    await workItem(stoppingToken);

                    _logger.LogInformation("Execution of {workItem} completed!!!", nameof(workItem));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occured executing {workItem}", nameof(workItem));
                }
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Queue Hosted service has been stopped!!!");

            await base.StopAsync(cancellationToken);
        }
    }
}
