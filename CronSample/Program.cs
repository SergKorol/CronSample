using System.Collections;
using CronSample.Configuration;
using CronSample.DI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CronSample;

public abstract class Program
{
    private Program() { }

    public static async Task<int> Main()
    {
        var appSettings = BuildAppSettingsFromEnvironmentVariables();

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .CreateLogger();

        var serviceProvider = new ServiceCollection()
            .AddSingleton(appSettings)
            .AddJobProcessor()
            .AddRestSharp()
            .AddLogging(cfg => cfg.AddSerilog())
            .BuildServiceProvider();

        try
        {
            await serviceProvider.GetServices<IJobProcessor>().ToDictionary(job => job.JobToProcess)[appSettings.JobToProcess].Execute();

            return 0;
        }
        catch (Exception exc)
        {
            serviceProvider.GetRequiredService<ILogger<Program>>().LogCritical(exc, "An error has occured during {Job} process", appSettings.JobToProcess);

            return 1;
        }
    }

    private static AppSettings BuildAppSettingsFromEnvironmentVariables()
    {
        IDictionary<string, object> environmentVariables = Environment.GetEnvironmentVariables()
            .Cast<DictionaryEntry>()
            .Where(entry => entry.Key is string)
            .GroupBy(entry => ((string)entry.Key).ToUpper())
            .ToDictionary(g => g.Key, g => g.Single().Value!);

        return new AppSettings(environmentVariables);
    }
}