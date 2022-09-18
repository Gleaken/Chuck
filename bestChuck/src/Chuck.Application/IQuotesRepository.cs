using Chuck.Application.Features.Quotes;
using Chuck.Application.Models;

namespace Chuck.Application;

public interface IQuotesRepository
{
    Task AddQuotesAsync(IEnumerable<QuoteDto> quotes, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(string id);
}