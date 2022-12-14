using Chuck.Application.Features.Quotes;

namespace Chuck.Application;

public interface IQuotesRepository
{
    Task AddQuotesAsync(IEnumerable<QuoteDto> quotes, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(string id);
}