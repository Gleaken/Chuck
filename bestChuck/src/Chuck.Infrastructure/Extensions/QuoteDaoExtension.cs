using Chuck.Application.Features.Quotes;
using Chuck.Application.Models;
using Chuck.Infrastructure.Models;

namespace Chuck.Infrastructure.Extensions;

public static class QuoteDtoExtension
{
    public static QuoteDao MapTo(this QuoteDto quoteDto) => new ()
    {
        Id = quoteDto.Id,
        Quote = quoteDto.Quote
    };
}