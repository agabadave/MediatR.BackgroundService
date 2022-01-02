namespace MediatR.BackgroundService.SampleApi.BusinessLogic.Handlers.InitiateOperation;

public record InitiateOperationRequest() : IRequest<InitiateOperationResponse>;

public record InitiateOperationResponse(string Value = default!);