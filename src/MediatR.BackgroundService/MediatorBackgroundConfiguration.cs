﻿using MediatR.BackgroundService.BackgroundServices;
using Microsoft.Extensions.DependencyInjection;

namespace MediatR.BackgroundService;

public static class MediatorBackgroundConfiguration
{
    public static IServiceCollection ConfigureBackgroundServices(this IServiceCollection services)
    {

        services.AddHostedService<QueueHostedService>();
        services.AddSingleton<IMediatorBackground, BackgroundTaskQueue>();
        return services;
    }
}
