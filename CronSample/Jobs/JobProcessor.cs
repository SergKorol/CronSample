using CronSample.Contracts;
using Microsoft.Extensions.Logging;

namespace CronSample.Jobs;

public class JobProcessor : JobProcessorBase
{
    private readonly IRestSharpService _restSharpService;

    public JobProcessor(IRestSharpService restSharpService, ILogger<JobProcessor> logger) : base(logger)
    {
        _restSharpService = restSharpService;
    }

    public override Job JobToProcess => Job.HealthJob;

    protected override async Task Process()
    {
        bool isHealthy = await _restSharpService.IsHealthy();
        Logger.LogInformation("Test endpoint is it Healthy : {IsHealthy}", isHealthy);
    }
}
