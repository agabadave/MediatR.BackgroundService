using System.Threading;
using System.Threading.Tasks;

namespace MediatR.BackgroundService;

public interface IMediatorBackground
{
    ValueTask Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
}
