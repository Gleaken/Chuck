using Chuck.Application.Features.Quotes;
using Microsoft.Extensions.Logging;

namespace Chuck.Application.Features.Filters;

public class QuoteLengthFilter : IQuoteFilter
{
    private readonly ILogger<QuoteLengthFilter> _logger;

    public QuoteLengthFilter(ILogger<QuoteLengthFilter> logger)
    {
        _logger = logger;
    }

    public async Task<bool> Passed(QuoteDto quote)
    {
        var retVal = await Task.FromResult(quote.Quote.Length <= 200);
        if(!retVal)
            _logger.LogWarning("Quote with id {Id} is too long {Length}", quote.Id, quote.Quote.Length);

        return retVal;
    }
}