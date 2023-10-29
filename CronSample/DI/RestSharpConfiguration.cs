using CronSample.Contracts;
using CronSample.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CronSample.DI;

public static class RestSharpConfiguration
{
    public static IServiceCollection AddRestSharp(this IServiceCollection services)
    {
        services.AddSingleton<IRestSharpService, RestSharpService>();

        return services;
    }
}
