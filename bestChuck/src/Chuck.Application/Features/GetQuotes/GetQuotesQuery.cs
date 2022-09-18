using Chuck.Application.Models;
using MediatR;

namespace Chuck.Application.Features.GetQuotes;

public record GetQuotesQuery() : IRequest<IEnumerable<QuoteDto>>;