using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Chuck.Worker.Http;

public interface IChuckHttpClient
{
    Task<ChuckResponse> GetQuote();
}

public class ChuckHttpClient : IChuckHttpClient
{
    private readonly HttpClient _client;
    private readonly string _url;
    private readonly ILogger<ChuckHttpClient> _logger;

    public ChuckHttpClient(HttpClient client, IOptions<WorkerConfig> options, ILogger<ChuckHttpClient> logger)
    {
        _client = client;
        _url = options.Value.QuoteUrl;
        _logger = logger;
    }

    public async Task<ChuckResponse> GetQuote()
    {
        try
        {
            var response = await _client.GetAsync(_url);
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                _logger.LogDebug("Received response with content {Content}", content);
                var chuckResponse = JsonConvert.DeserializeObject<ChuckResponse>(content);
                return chuckResponse;
            }

            _logger.LogError("Error response {code} with message {message}", response.StatusCode, content);
        }
        catch (Exception ex)
        {
            _logger.LogError("{Exception}", ex);
        }
        
        return await Task.FromResult<ChuckResponse>(null);
    }
}