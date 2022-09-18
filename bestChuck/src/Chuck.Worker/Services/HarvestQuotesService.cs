using Chuck.Application.Features.Quotes;
using Chuck.Worker.Extensions;
using Chuck.Worker.Http;
using MediatR;

namespace Chuck.Worker.Services;

public interface IHarvestQuotesService
{
    Task Harvest(int count, CancellationToken stoppingToken);
}

public class HarvestQuotesService : IHarvestQuotesService
{
    private readonly IChuckHttpClient _client;
    private readonly IMediator _mediator;
    

    public HarvestQuotesService(IChuckHttpClient client, IMediator mediator)
    {
        _client = client;
        _mediator = mediator;
    }

    public async Task Harvest(int count, CancellationToken stoppingToken)
    {
        var responses = await HarvestQuotes(count);

        if (!responses.Any()) return;

        var addQuotesCommand =
            new AddQuotesCommand(responses
                .Select(x => x.MapToQuoteDto())
                .ToList());

        await _mediator.Send(addQuotesCommand, stoppingToken);
    }

    private async Task<List<ChuckResponse>> HarvestQuotes(int count)
    {
        var responses = new List<Task<ChuckResponse>>();
        for (var i = 0; i < count; i++)
        {
            responses.Add(_client.GetQuote());
        }

        await Task.WhenAll(responses);
        return responses.Where(x => x.Result is not null)
            .Select(x => x.Result)
            .ToList();
    }
}