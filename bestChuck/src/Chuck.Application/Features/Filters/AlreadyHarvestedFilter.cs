using Chuck.Application.Features.Quotes;
using Microsoft.Extensions.Logging;

namespace Chuck.Application.Features.Filters;

public class AlreadyHarvestedFilter : IQuoteFilter
{
    private readonly IQuotesRepository _collection;
    private readonly ILogger<AlreadyHarvestedFilter> _logger;

    public AlreadyHarvestedFilter(IQuotesRepository collection, ILogger<AlreadyHarvestedFilter> logger)
    {
        _collection = collection;
        _logger = logger;
    }

    public async Task<bool> Passed(QuoteDto quote)
    {
        var exists = await _collection.ExistsAsync(quote.Id);
        if(exists)
            _logger.LogWarning("Quote with id {Id} already in harvested", quote.Id);

        return !exists;
    } 
}