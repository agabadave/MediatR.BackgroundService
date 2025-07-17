using MediatR.BackgroundService.SampleApi.BusinessLogic.Handlers.LongOperation;

namespace MediatR.BackgroundService.SampleApi.BusinessLogic.Handlers.InitiateOperation;

public class InitiateOperationHandler(IMediatorBackground mediatorBackground) : IRequestHandler<InitiateOperationRequest, InitiateOperationResponse>
{
    private readonly IMediatorBackground _mediatorBackground = mediatorBackground;

    public async Task<InitiateOperationResponse> Handle(InitiateOperationRequest request, CancellationToken cancellationToken)
    {
        LongOperationRequest backgroundRequest = new("Handler");

        await _mediatorBackground.Send(backgroundRequest, default);

        var response = new InitiateOperationResponse
        {
            Value = "Initiation from handler has been successful."
        };

        return response;
    }

}
