using RestSharp;

namespace CronSample.Services;

public abstract class RestSharpClient : IDisposable
{
    private readonly RestClient _client;

    protected RestSharpClient(string baseUrl)
    {
        _client = new RestClient(baseUrl);
    }

    protected async Task<string> ExecuteAsyncThrowingEvenForNotFound(RestRequest request, Method method)
    {
        var resp = await _client.ExecuteAsync(request, method);

        if (!resp.IsSuccessStatusCode || resp.ErrorException is not null)
        {
            throw resp.ErrorException ?? new InvalidOperationException($"Not a success HTTP StatusCode: {resp.StatusCode}");
        }

        return "Healthy";
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            _client.Dispose();
        }
    }
}
