using MediatR.BackgroundService.BackgroundServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediatR.BackgroundService
{
    public static class MediatorBackgroundConfiguration
    {
        public static IServiceCollection ConfigureBackgroundServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddHostedService<QueueHostedService>();
            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
            services.AddSingleton<IMediatorBackground, MediatorBackground>();
            return services;
        }
    }
}
