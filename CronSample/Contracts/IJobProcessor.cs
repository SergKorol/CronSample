namespace CronSample;

public interface IJobProcessor
{
    Job JobToProcess { get; }
    Task Execute();
}

