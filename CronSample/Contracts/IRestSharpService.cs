namespace CronSample.Contracts;

public interface IRestSharpService
{
    Task<bool> IsHealthy();
}
