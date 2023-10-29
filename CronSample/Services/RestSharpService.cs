using CronSample.Configuration;
using CronSample.Contracts;
using RestSharp;

namespace CronSample.Services;

public class RestSharpService : RestSharpClient, IRestSharpService
{
    public RestSharpService(AppSettings appSettings) : base(appSettings.TestEndpointConfiguration.BaseUrl)
    {
    }

    public async Task<bool> IsHealthy()
    {
        RestRequest request = new RestRequest("/");
        return await ExecuteAsyncThrowingEvenForNotFound(request, Method.Get) == "Healthy";
    }
}
