using Chuck.Worker.Services;
using Microsoft.Extensions.Options;

namespace Chuck.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly WorkerConfig _config;
    private readonly IServiceProvider _serviceProvider;

    public Worker(IOptions<WorkerConfig> config, ILogger<Worker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _config = config.Value;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogDebug("Start harvesting: {time}", DateTimeOffset.Now);
            
            await HarvestQuotes(stoppingToken);
            
            _logger.LogDebug("Harvesting ended: {time}", DateTimeOffset.Now);
            await Task.Delay(_config.Frequency, stoppingToken);
        }
    }

    private async Task HarvestQuotes(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IHarvestQuotesService>();
        await service.Harvest(_config.FetchCount, stoppingToken);
    }
}
