# MediatR.BackgroundService

This library extends [MediatR](https://github.com/jbogard/MediatR) to enable the mediator pattern for background services in .NET applications. It uses a queue-based background task system, as described in the [Microsoft Docs](https://learn.microsoft.com/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-9.0#queued-background-tasks).

## Supported Versions

- .NET 9.0
- MediatR 12.5.0 (restricted due to licensing changes in later versions)
- Microsoft.Extensions.* 9.x

## How it Works

- Background tasks are enqueued using an `IBackgroundTaskQueue` implementation.
- Tasks are processed by a hosted service running in the background.
- You can send MediatR requests to be handled asynchronously, offloading long-running or resource-intensive operations from the main request pipeline.
- The queue is implemented using `System.Threading.Channels` for efficient, thread-safe task management.

## Usage

You can enqueue MediatR requests to be processed in the background from either a controller or another handler. Below are examples for both scenarios:

### From a Controller

```csharp
[ApiController]
[Route("api/[controller]")]
public class InitiateController : ControllerBase
{
    private readonly IMediatRBackground _mediatorBackground;
    // ... constructor omitted for brevity ...

    [HttpPost("from-controller")]
    public async Task<ActionResult> InitiateCall()
    {
        var request = new LongOperationRequest(Source: "Controller");
        await _mediatorBackground.Send(request, default);
        return Accepted("Action has been initiated. Check Logs for details...");
    }
}
```

### From a Handler

```csharp
public class InitiateOperationHandler : IRequestHandler<InitiateOperationRequest, InitiateOperationResponse>
{
    private readonly IMediatRBackground _mediatorBackground;
    // ... constructor omitted for brevity ...

    public async Task<InitiateOperationResponse> Handle(InitiateOperationRequest request, CancellationToken cancellationToken)
    {
        var backgroundRequest = new LongOperationRequest("Handler");
        await _mediatorBackground.Send(backgroundRequest, default);
        return new InitiateOperationResponse
        {
            Value = "Initiation from handler has been successful."
        };
    }
}
```

This approach allows you to offload long-running or resource-intensive operations to background processing, improving the responsiveness of your API endpoints.

## License Note

MediatR is restricted to v12.5.0 in this library due to a licensing change in later versions. Please review the [MediatR license](https://github.com/jbogard/MediatR/blob/master/LICENSE) if you plan to upgrade further.



