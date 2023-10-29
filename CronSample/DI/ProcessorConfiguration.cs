using CronSample.Jobs;
using Microsoft.Extensions.DependencyInjection;

namespace CronSample.DI;

public static class ProcessorConfiguration
{
    public static IServiceCollection AddJobProcessor(this IServiceCollection services)
    {
        services.AddTransient<IJobProcessor, JobProcessor>();

        return services;
    }
}
