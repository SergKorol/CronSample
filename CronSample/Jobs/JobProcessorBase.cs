using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace CronSample.Jobs;

public abstract class JobProcessorBase : IJobProcessor
{
    protected readonly ILogger Logger;

    protected JobProcessorBase(ILogger logger)
    {
        Logger = logger;
    }

    protected abstract Task Process();

    public abstract Job JobToProcess { get; }

    public async Task Execute()
    {
        var stopwatch = Stopwatch.StartNew();
        Logger.LogInformation("{JobToProcess} started", JobToProcess.ToString());

        await Process();

        stopwatch.Stop();
        Logger.LogInformation("{JobToProcess} ended, elapsed time: {ElapsedMilliseconds} ms", JobToProcess, stopwatch.ElapsedMilliseconds);
    }
}
