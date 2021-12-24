using MediatR.BackgroundService.SampleApi.BusinessLogic.Handlers.InitiateOperation;
using MediatR.BackgroundService.SampleApi.BusinessLogic.Handlers.LongOperation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediatR.BackgroundService.SampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitiateController : ControllerBase
    {
        private readonly IMediatorBackground _mediatorBackground;
        private readonly Mediator _mediator;

        public InitiateController(IMediatorBackground mediatorBackground, Mediator mediator)
        {
            _mediatorBackground = mediatorBackground;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> InitiateCall()
        {
            var request = new LongOperationRequest();

            await _mediatorBackground.Send(request);

            return Accepted("Action has been initiated. Check Logs for details...");
        }

        [HttpPost("from-handler")]
        public async Task<ActionResult<InitiateOperationResponse>> InitiateFromHandler(InitiateOperationRequest request)
        {
            var response = await _mediator.Send(request);

            return Accepted(response);
        }
    }
}
