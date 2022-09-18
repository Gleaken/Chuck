using Chuck.Application.Features.Quotes;

namespace Chuck.Application.Features.Filters;

public interface IQuoteFilter
{
    Task<bool> Passed(QuoteDto quote);
}