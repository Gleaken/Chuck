using Chuck.Application.Features.Quotes;
using Chuck.Application.Models;
using Chuck.Worker.Http;

namespace Chuck.Worker.Extensions;

public static class ChuckResponseExtension
{
    public static QuoteDto MapToQuoteDto(this ChuckResponse chuckResponse) => new ()
    {
        Id = chuckResponse.Id,
        Quote = chuckResponse.Value
    };
}