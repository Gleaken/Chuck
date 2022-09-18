using Chuck.Application.Features.Quotes;
using Chuck.Application.Models;

namespace Chuck.Application.Features.Filters;

public interface IQuoteFilter
{
    Task<bool> Passed(QuoteDto quote);
}