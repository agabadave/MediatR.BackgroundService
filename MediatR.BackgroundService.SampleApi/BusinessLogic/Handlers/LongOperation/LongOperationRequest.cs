namespace MediatR.BackgroundService.SampleApi.BusinessLogic.Handlers.LongOperation;

/// <summary>
/// A request for the long operation. This will be ideally executed in the background
/// </summary>
public record LongOperationRequest(string Source) : IRequest<Unit>;
