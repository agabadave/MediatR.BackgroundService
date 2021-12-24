namespace MediatR.BackgroundService.SampleApi.BusinessLogic.Handlers.LongOperation
{
    public class LongOperationHandler : IRequestHandler<LongOperationRequest, Unit>
    {
        private readonly ILogger<LongOperationHandler> _logger;

        public LongOperationHandler(ILogger<LongOperationHandler> logger)
        {
            _logger = logger;
        }

        public async Task<Unit> Handle(LongOperationRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started long operation");

            for (int i = 0; i < 100; i++)
            {
                await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);

                _logger.LogInformation($"Long operation running {1}/100");
            }

            return Unit.Value;
        }
    }
}
