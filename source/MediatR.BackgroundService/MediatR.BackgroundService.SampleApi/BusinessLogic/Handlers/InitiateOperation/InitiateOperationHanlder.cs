using MediatR.BackgroundService.SampleApi.BusinessLogic.Handlers.LongOperation;

namespace MediatR.BackgroundService.SampleApi.BusinessLogic.Handlers.InitiateOperation
{
    public class InitiateOperationHanlder : IRequestHandler<InitiateOperationRequest, InitiateOperationResponse>
    {
        private readonly IMediatorBackground _mediatorBackground;

        public InitiateOperationHanlder(IMediatorBackground mediatorBackground)
        {
            _mediatorBackground = mediatorBackground;
        }

        public async Task<InitiateOperationResponse> Handle(InitiateOperationRequest request, CancellationToken cancellationToken)
        {
            var backgroundRequest = new LongOperationRequest();

            await _mediatorBackground.Send(backgroundRequest);

            var response = new InitiateOperationResponse 
            { 
                Value = "Initiation from handler has been successful."
            };

            return response;
        }

    }
}
